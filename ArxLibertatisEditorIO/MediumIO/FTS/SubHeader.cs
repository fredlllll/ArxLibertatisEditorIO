using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class SubHeader
    {
        public string path;
        public byte[] checksum;

        internal void ReadFrom(FTS_IO_UNIQUE_HEADER2 header)
        {
            path = IOHelper.GetString(header.path);
            checksum = header.check;
        }

        internal void WriteTo(ref FTS_IO_UNIQUE_HEADER2 header)
        {
            header.path = IOHelper.GetBytes(path, 256);
            header.check = checksum;
            IOHelper.EnsureArraySize(ref header.check, 512, true);
        }

        public override string ToString()
        {
            return $"Path: {path}";
        }
    }
}
