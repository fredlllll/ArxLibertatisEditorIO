﻿using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.LLF
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class LLF_IO_HEADER
    {
        public float version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] identifier;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] lastuser;
        public uint time; //originally signed int. same reason as in dlf header
        public int numLights;
        public int numShadowPolys;
        public int numIgnoredPolys;
        public int numBackgroundPolys;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public int[] ipad1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public float[] fpad;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4096)]
        public byte[] cpad;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public int[] ipad2;
    }
}
