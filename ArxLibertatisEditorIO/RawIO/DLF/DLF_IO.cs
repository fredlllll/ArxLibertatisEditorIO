using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System.IO;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    public class DLF_IO
    {
        public DLF_IO_HEADER header;
        public DLF_IO_SCENE[] scenes;
        public DLF_IO_INTER[] inters;
        public DANAE_IO_LIGHTINGHEADER lightingHeader;
        public uint[] lightColors;
        public DANAE_IO_LIGHT[] lights;
        public DLF_IO_FOG[] fogs;
        public byte[] nodesData;
        public DLF_IO_PATH[] paths;

        public void ReadFrom(Stream stream)
        {
            using StructReader reader = new StructReader(stream, System.Text.Encoding.ASCII, true);

            header = reader.ReadStruct<DLF_IO_HEADER>();

            scenes = new DLF_IO_SCENE[header.numScenes];
            for (int i = 0; i < header.numScenes; i++)
            {
                scenes[i] = reader.ReadStruct<DLF_IO_SCENE>();
            }

            inters = new DLF_IO_INTER[header.numInters];
            for (int i = 0; i < header.numInters; i++)
            {
                inters[i] = reader.ReadStruct<DLF_IO_INTER>();
            }

            if (header.lighting != 0)
            {
                lightingHeader = reader.ReadStruct<DANAE_IO_LIGHTINGHEADER>();

                lightColors = new uint[lightingHeader.numLights];
                for (int i = 0; i < lightingHeader.numLights; i++)
                {
                    lightColors[i] = reader.ReadUInt32(); //TODO is apparently BGRA if its in compact mode.
                }
            }

            lights = new DANAE_IO_LIGHT[header.numLights];
            for (int i = 0; i < header.numLights; i++)
            {
                lights[i] = reader.ReadStruct<DANAE_IO_LIGHT>();
            }

            fogs = new DLF_IO_FOG[header.numFogs];
            for (int i = 0; i < header.numFogs; i++)
            {
                fogs[i] = reader.ReadStruct<DLF_IO_FOG>();
            }

            // Skip nodes, dont know why
            //save in var so we can write it back later
            nodesData = reader.ReadBytes(header.numNodes * (204 + header.numNodelinks * 64));

            paths = new DLF_IO_PATH[header.numPaths];
            for (int i = 0; i < header.numPaths; i++)
            {
                var path = new DLF_IO_PATH();
                path.ReadFrom(reader);
                paths[i] = path;
            }
        }

        public void WriteTo(Stream stream)
        {
            header.numScenes = scenes.Length;
            header.numInters = inters.Length;
            header.numLights = lights.Length;
            header.numFogs = fogs.Length;
            header.numPaths = paths.Length;

            using StructWriter writer = new StructWriter(stream, System.Text.Encoding.ASCII, true);

            writer.WriteStruct(header);

            for (int i = 0; i < scenes.Length; i++)
            {
                writer.WriteStruct(scenes[i]);
            }

            for (int i = 0; i < inters.Length; i++)
            {
                writer.WriteStruct(inters[i]);
            }

            if (header.lighting != 0)
            {
                lightingHeader.numLights = lightColors.Length;
                writer.WriteStruct(lightingHeader);

                for (int i = 0; i < lightColors.Length; i++)
                {
                    writer.Write(lightColors[i]);
                }
            }

            for (int i = 0; i < lights.Length; i++)
            {
                writer.WriteStruct(lights[i]);
            }

            for (int i = 0; i < fogs.Length; i++)
            {
                writer.WriteStruct(fogs[i]);
            }

            //write back nodes data
            writer.Write(nodesData);

            for (int i = 0; i < paths.Length; i++)
            {
                paths[i].WriteTo(writer);
            }
        }

        public static Stream EnsureUnpacked(Stream stream)
        {
            using var reader = new StructReader(stream, System.Text.Encoding.ASCII, true);

            var version = reader.ReadSingle(); //read just version
            stream.Position -= sizeof(float);//back to start for further processing
            if (version >= 1.44f)
            {
                var headerSize = Marshal.SizeOf<DLF_IO_HEADER>();
                return CompressionUtil.EnsureUncompressed(stream, headerSize, stream.Length - headerSize);
            }
            return stream; //no need to unpack, return input stream
        }

        public static Stream EnsurePacked(Stream stream)
        {
            var headerSize = Marshal.SizeOf<DLF_IO_HEADER>();
            return CompressionUtil.EnsureCompressed(stream, headerSize, stream.Length - headerSize);
        }
    }
}
