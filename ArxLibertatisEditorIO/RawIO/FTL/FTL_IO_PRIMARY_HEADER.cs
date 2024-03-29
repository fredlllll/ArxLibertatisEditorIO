﻿using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTL_IO_PRIMARY_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] ident; // FTL\0
        public float version; // 0.83257f
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] checksum; // not read by game, so we luckily dont have to provide it
    }
}
