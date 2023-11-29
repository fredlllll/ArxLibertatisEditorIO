﻿using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTL_IO_3D_DATA_GROUP
    {
        public EERIE_GROUP_FTL group;
        public int[] indices;
    }
}
