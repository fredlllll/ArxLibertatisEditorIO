using ArxLibertatisEditorIO.RawIO.FTL;
using ArxLibertatisEditorIO.Util;
using System;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class PrimaryHeader
    {
        public string identifier = "FTL";
        public float version = 0.83257f;
        public byte[] checksum = new byte[512];
        public void ReadFrom(FTL_IO_PRIMARY_HEADER header)
        {
            identifier = IOHelper.GetString(header.ident);
            version = header.version;
            checksum = header.checksum;
        }

        public void WriteTo(ref FTL_IO_PRIMARY_HEADER header)
        {
            header.ident = IOHelper.GetBytes(identifier, 4);
            header.version = version;
            IOHelper.EnsureArraySize(ref header.checksum, 512);
            Array.Copy(checksum, header.checksum, 512);
        }
    }
}