using ArxLibertatisEditorIO.RawIO.Shared;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct DLF_IO_INTER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] name;
        public SavedVec3 pos;
        public SavedAnglef angle;
        public int ident;
        public int flags; //apparently completely unused on level load
        public fixed int ipad[14];
        public fixed float fpad[16];
    }
}
