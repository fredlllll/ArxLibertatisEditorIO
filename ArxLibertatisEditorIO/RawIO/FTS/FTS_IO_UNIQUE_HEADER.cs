﻿using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct FTS_IO_UNIQUE_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] path;
        public int count;
        public float version;
        public int uncompressedsize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] pad;
    }
}
