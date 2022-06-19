using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTL
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct EERIE_FACE_FTL
    {
        public PolyType facetype; // 0 = flat, 1 = text, 2 = Double-Side
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] rgb;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ushort[] vid;
        public short texid;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] u;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] v;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] ou;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] ov;
        public float transval;
        public SavedVec3 norm;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public SavedVec3[] nrmls;
        public float temp;
    }
}
