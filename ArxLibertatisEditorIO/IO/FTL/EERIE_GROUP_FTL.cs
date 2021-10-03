﻿using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.IO.FTL
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct EERIE_GROUP_FTL
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] name;
        public int origin; // TODO this is always positive use u32 ?
        public int nb_index;
        public int indexes;
        public float siz;
    }
}
