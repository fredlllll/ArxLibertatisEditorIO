using ArxLibertatisEditorIO.RawIO.FTL;
using ArxLibertatisEditorIO.Util;
using System;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class DataSection3DHeader
    {
        public int origin = 0;
        public string name = "noname";
        public void ReadFrom(FTL_IO_3D_DATA_HEADER header)
        {
            origin = header.origin;
            name = IOHelper.GetString(header.name);
        }

        public void WriteTo(ref FTL_IO_3D_DATA_HEADER header)
        {
            header.origin = origin;
            header.name = IOHelper.GetBytes(name, 256);
        }
    }
}