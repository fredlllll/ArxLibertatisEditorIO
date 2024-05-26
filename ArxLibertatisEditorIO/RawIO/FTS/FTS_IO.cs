using ArxLibertatisEditorIO.Util;
using System.IO;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    public class FTS_IO
    {
        public FTS_IO_UNIQUE_HEADER header;
        public FTS_IO_UNIQUE_HEADER2[] uniqueHeaders;

        public FTS_IO_SCENE_HEADER sceneHeader;

        public FTS_IO_TEXTURE_CONTAINER[] textureContainers;
        public FTS_IO_CELL[] cells;
        public FTS_IO_ANCHOR[] anchors;
        public EERIE_IO_PORTALS[] portals;
        public FTS_IO_ROOM[] rooms;
        public FTS_IO_ROOM_DIST_DATA[] roomDistances;

        public void ReadFrom(Stream stream)
        {
            using StructReader reader = new StructReader(stream, System.Text.Encoding.ASCII, true);

            header = reader.ReadStruct<FTS_IO_UNIQUE_HEADER>();

            uniqueHeaders = new FTS_IO_UNIQUE_HEADER2[header.count];
            for (int i = 0; i < header.count; i++)
            {
                uniqueHeaders[i] = reader.ReadStruct<FTS_IO_UNIQUE_HEADER2>();
            }

            sceneHeader = reader.ReadStruct<FTS_IO_SCENE_HEADER>();

            textureContainers = new FTS_IO_TEXTURE_CONTAINER[sceneHeader.nb_textures];
            for (int i = 0; i < sceneHeader.nb_textures; i++)
            {
                textureContainers[i] = reader.ReadStruct<FTS_IO_TEXTURE_CONTAINER>();
            }

            int cellCount = sceneHeader.sizez * sceneHeader.sizex;
            cells = new FTS_IO_CELL[cellCount];
            for (int i = 0; i < cellCount; i++)
            {
                var cell = new FTS_IO_CELL();
                cell.ReadFrom(reader);
                cells[i] = cell;
            }

            anchors = new FTS_IO_ANCHOR[sceneHeader.nb_anchors];
            for (int i = 0; i < sceneHeader.nb_anchors; i++)
            {
                var anchor = new FTS_IO_ANCHOR();
                anchor.ReadFrom(reader);
                anchors[i] = anchor;
            }

            portals = new EERIE_IO_PORTALS[sceneHeader.nb_portals];
            for (int i = 0; i < sceneHeader.nb_portals; i++)
            {
                portals[i] = reader.ReadStruct<EERIE_IO_PORTALS>();
            }

            rooms = new FTS_IO_ROOM[sceneHeader.nb_rooms + 1];
            for (int i = 0; i < sceneHeader.nb_rooms + 1; i++) //no idea why +1, but its in the code
            {
                var room = new FTS_IO_ROOM();
                room.ReadFrom(reader);
                rooms[i] = room;
            }

            roomDistances = new FTS_IO_ROOM_DIST_DATA[rooms.Length * rooms.Length];
            for (int i = 0, index = 0; i < rooms.Length; i++)
            {
                for (int j = 0; j < rooms.Length; j++, index++)
                {
                    roomDistances[index] = reader.ReadStruct<FTS_IO_ROOM_DIST_DATA>();
                }
            }

            long remaining = reader.BaseStream.Length - reader.BaseStream.Position;
            if (remaining > 0)
            {
                Logging.LogWarning("ignoring " + remaining + " bytes in fts");
            }
        }

        public int CalculatePolyCount()
        {
            int count = 0;

            for (int i = 0; i < cells.Length; i++)
            {
                count += cells[i].polygons.Length;
            }

            return count;
        }

        public void WriteTo(Stream stream, bool calculateUncompressedSize = true)
        {
            header.count = uniqueHeaders.Length;

            sceneHeader.nb_textures = textureContainers.Length;
            sceneHeader.nb_polys = CalculatePolyCount();
            sceneHeader.nb_anchors = anchors.Length;
            sceneHeader.nb_portals = portals.Length;
            sceneHeader.nb_rooms = rooms.Length - 1;

            if (calculateUncompressedSize) {
                header.uncompressedsize = GetUncompressedSize();
            }

            using StructWriter writer = new StructWriter(stream, System.Text.Encoding.ASCII, true);

            writer.WriteStruct(header);

            for (int i = 0; i < uniqueHeaders.Length; i++)
            {
                writer.WriteStruct(uniqueHeaders[i]);
            }

            writer.WriteStruct(sceneHeader);

            for (int i = 0; i < textureContainers.Length; i++)
            {
                writer.WriteStruct(textureContainers[i]);
            }

            int cellCountShould = sceneHeader.sizex * sceneHeader.sizez;
            if (cells.Length != cellCountShould)
            {
                Logging.LogWarning($"cells length is {cells.Length} but should be {cellCountShould}");
            }
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].WriteTo(writer);
            }

            for (int i = 0; i < anchors.Length; i++)
            {
                anchors[i].WriteTo(writer);
            }

            for (int i = 0; i < portals.Length; i++)
            {
                writer.WriteStruct(portals[i]);
            }

            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i].WriteTo(writer);
            }

            for (int i = 0; i < roomDistances.Length; i++)
            {
                writer.WriteStruct(roomDistances[i]);
            }
        }

        int GetUncompressedSize()
        {
            int size = 0;

            //headers are not included in uncompressed size field
            //size += Marshal.SizeOf<FTS_IO_UNIQUE_HEADER>();
            //size += Marshal.SizeOf<FTS_IO_UNIQUE_HEADER2>() * uniqueHeaders.Length;

            size += Marshal.SizeOf<FTS_IO_SCENE_HEADER>();

            size += Marshal.SizeOf<FTS_IO_TEXTURE_CONTAINER>() * textureContainers.Length;

            for (int i = 0; i < cells.Length; i++)
            {
                var c = cells[i];
                size += c.CalculateWrittenSize();
            }

            for (int i = 0; i < anchors.Length; i++)
            {
                var a = anchors[i];
                size += a.CalculateWrittenSize();
            }

            size += Marshal.SizeOf<EERIE_IO_PORTALS>() * portals.Length;

            for (int i = 0; i < rooms.Length; i++)
            {
                var r = rooms[i];
                size += r.CalculateWrittenSize();
            }

            size += Marshal.SizeOf<FTS_IO_ROOM_DIST_DATA>() * roomDistances.Length;

            return size;
        }

        static bool IsCompressed(Stream stream, out long headerSize)
        {
            long start = stream.Position;
            using var reader = new StructReader(stream, System.Text.Encoding.ASCII, true);

            var header = reader.ReadStruct<FTS_IO_UNIQUE_HEADER>();
            headerSize = Marshal.SizeOf<FTS_IO_UNIQUE_HEADER>();
            headerSize += header.count * Marshal.SizeOf<FTS_IO_UNIQUE_HEADER2>();
            stream.Position = start;
            return header.uncompressedsize != 0;
        }

        static long GetHeaderSize(Stream stream)
        {
            long start = stream.Position;
            using var reader = new StructReader(stream, System.Text.Encoding.ASCII, true);

            var header = reader.ReadStruct<FTS_IO_UNIQUE_HEADER>();
            long size = Marshal.SizeOf<FTS_IO_UNIQUE_HEADER>();
            size += header.count * Marshal.SizeOf<FTS_IO_UNIQUE_HEADER2>();
            stream.Position = start;
            return size;
        }

        public static Stream EnsureUnpacked(Stream s)
        {
            if (IsCompressed(s, out long headerSize))
            {
                return CompressionUtil.EnsureUncompressed(s, headerSize, s.Length - headerSize);
            }
            return s;
        }

        public static Stream EnsurePacked(Stream s)
        {
            long headerSize = GetHeaderSize(s);
            return CompressionUtil.EnsureCompressed(s, headerSize, s.Length - headerSize);
        }
    }
}
