using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System.IO;

namespace ArxLibertatisEditorIO.RawIO.LLF
{
    public class LLF_IO
    {
        public LLF_IO_HEADER header;
        public DANAE_IO_LIGHT[] lights;
        public DANAE_IO_LIGHTINGHEADER lightingHeader;
        public uint[] lightColors;

        public void ReadFrom(Stream s)
        {
            using var reader = new StructReader(s, System.Text.Encoding.ASCII, true);

            header = reader.ReadStruct<LLF_IO_HEADER>();

            lights = new DANAE_IO_LIGHT[header.numLights];
            for (int i = 0; i < header.numLights; i++)
            {
                lights[i] = reader.ReadStruct<DANAE_IO_LIGHT>();
            }

            lightingHeader = reader.ReadStruct<DANAE_IO_LIGHTINGHEADER>();

            lightColors = new uint[lightingHeader.numLights];
            for (int i = 0; i < lightingHeader.numLights; i++)
            {
                lightColors[i] = reader.ReadUInt32(); //is apparently BGRA if its in compact mode, which it always is
            }
        }

        public void WriteTo(Stream s)
        {
            using var writer = new StructWriter(s, System.Text.Encoding.ASCII, true);

            header.numLights = lights.Length;
            lightingHeader.numLights = lightColors.Length;

            writer.WriteStruct(header);

            for (int i = 0; i < lights.Length; i++)
            {
                writer.WriteStruct(lights[i]);
            }

            writer.WriteStruct(lightingHeader);

            for (int i = 0; i < lightColors.Length; i++)
            {
                writer.Write(lightColors[i]);
            }
        }

        public static Stream EnsureUnpacked(Stream s)
        {
            return CompressionUtil.EnsureUncompressed(s, 0, s.Length);
        }

        public static Stream EnsurePacked(Stream s)
        {
            return CompressionUtil.EnsureCompressed(s, 0, s.Length);
        }
    }
}
