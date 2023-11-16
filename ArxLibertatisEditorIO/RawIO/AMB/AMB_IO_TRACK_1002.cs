using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.RawIO.AMB
{
    public class AMB_IO_TRACK_1002 : AMB_IO_TRACK_BASE
    {
        public override void ReadFrom(StructReader reader)
        {
            sampleName = IOHelper.ReadVariableLengthString(reader);
            flags = (AMB_IO_TRACK_FLAGS)reader.ReadUInt32();
            keyCount = reader.ReadUInt32();

            keys = new AMB_IO_KEY[keyCount];
            for (int i = 0; i < keyCount; i++)
            {
                keys[i] = reader.ReadStruct<AMB_IO_KEY>();
            }
        }

        public override void WriteTo(StructWriter writer)
        {
            keyCount = (uint)keys.Length;

            writer.Write(sampleName);
            writer.Write((uint)flags);
            writer.Write(keyCount);

            for (int i = 0; i < keyCount; i++)
            {
                writer.WriteStruct(keys[i]);
            }
        }
    }
}
