using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.CIN
{
    public class CIN_IO_BITMAP
    {
        public int scale;
        public byte[] bitmapPath;

        public void ReadFrom(StructReader reader)
        {
            scale = reader.ReadInt32();
            bitmapPath = IOHelper.ReadVariableLengthString(reader);
        }

        public void WriteTo(StructWriter writer)
        {
            writer.Write(scale);
            writer.Write(bitmapPath);
        }
    }
}
