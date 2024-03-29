﻿using ArxLibertatisEditorIO.Util;

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
