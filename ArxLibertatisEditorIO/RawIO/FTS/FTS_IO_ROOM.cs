using ArxLibertatisEditorIO.Util;
using System.Numerics;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    public struct FTS_IO_ROOM
    {
        public EERIE_IO_ROOM_DATA data;
        public int[] portals;
        public FTS_IO_EP_DATA[] polygons;

        public void ReadFrom(StructReader reader)
        {
            data = reader.ReadStruct<EERIE_IO_ROOM_DATA>();

            portals = new int[data.nb_portals];
            for (int i = 0; i < data.nb_portals; i++)
            {
                portals[i] = reader.ReadInt32();
            }

            polygons = new FTS_IO_EP_DATA[data.nb_polys];
            for (int i = 0; i < data.nb_polys; i++)
            {
                polygons[i] = reader.ReadStruct<FTS_IO_EP_DATA>();
            }
        }

        public void WriteTo(StructWriter writer)
        {
            data.nb_polys = polygons == null ? 0 : polygons.Length;
            data.nb_portals = portals == null ? 0 : portals.Length;
            writer.WriteStruct(data);

            for (int i = 0; i < data.nb_portals; i++)
            {
                writer.Write(portals[i]);
            }

            for (int i = 0; i < data.nb_polys; i++)
            {
                writer.WriteStruct(polygons[i]);
            }
        }

        public int CalculateWrittenSize()
        {
            int size = 0;

            size += Marshal.SizeOf<EERIE_IO_ROOM_DATA>();
            size += sizeof(int) * portals.Length;
            size += Marshal.SizeOf<FTS_IO_EP_DATA>() * polygons.Length;

            return size;
        }

        public Vector3 CalculateCenter(FTS_IO fts)
        {
            int count = 0;
            float x = 0;
            float y = 0;
            float z = 0;

            for (int i = 0; i < polygons.Length; ++i)
            {
                var poly = polygons[i];
                var cellIndex = IOHelper.XZToCellIndex(poly.cell_x, poly.cell_z, fts.sceneHeader.sizex, fts.sceneHeader.sizez);
                var cell = fts.cells[cellIndex];
                var p = cell.polygons[poly.idx];

                var vertCount = p.type.HasFlag(PolyType.QUAD) ? 4 : 3;
                for (int j = 0; j < vertCount; ++j)
                {
                    x += p.vertices[j].posX;
                    y += p.vertices[j].posY;
                    z += p.vertices[j].posZ;
                    ++count;
                }
            }
            return new Vector3(x / count, y / count, z / count);
        }
    }
}
