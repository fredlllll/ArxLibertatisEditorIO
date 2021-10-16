using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct DLF_IO_PATH_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] name;
        public short idx;
        public PathFlags flags;
        public SavedVec3 initPos;
        public SavedVec3 pos;
        internal int numPathways;
        public SavedColor rgb;
        public float farClip;
        public float reverb;
        public float ambientMaxVolume;
        public fixed float fpad[26];
        public int height;
        public fixed int ipad[31];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] ambiance;
        public fixed byte cpad[128];
    }
}
