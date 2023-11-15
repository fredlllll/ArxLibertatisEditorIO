using System.IO;

namespace ArxLibertatisEditorIO.RawIO.AEF
{
    public class AEF_IO
    {
        public float size;
        public float diffusion;
        public float absorption;
        public float reflect_volume;
        public float reflect_delay;
        public float reverb_volume;
        public float reverb_delay;
        public float reverb_decay;
        public float reverb_hf_decay;

        public void ReadFrom(Stream dataStream)
        {
            using BinaryReader reader = new BinaryReader(dataStream);
            size = reader.ReadSingle();
            diffusion = reader.ReadSingle();
            absorption = reader.ReadSingle();
            reflect_volume = reader.ReadSingle();
            reflect_delay = reader.ReadSingle();
            reverb_volume = reader.ReadSingle();
            reverb_delay = reader.ReadSingle();
            reverb_decay = reader.ReadSingle();
            reverb_hf_decay = reader.ReadSingle();
        }

        public void WriteTo(Stream dataStream)
        {
            using BinaryWriter writer = new BinaryWriter(dataStream);
            writer.Write(size);
            writer.Write(diffusion);
            writer.Write(absorption);
            writer.Write(reflect_volume);
            writer.Write(reflect_delay);
            writer.Write(reverb_volume);
            writer.Write(reverb_delay);
            writer.Write(reverb_decay);
            writer.Write(reverb_hf_decay);
        }
    }
}
