using ArxLibertatisEditorIO.RawIO.Shared;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.CIN
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct CIN_IO_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] // must be "KFA\0"
        public byte[] identifier;
        public int version; //should be >= 1.75 and <= 1.76
    }
}
