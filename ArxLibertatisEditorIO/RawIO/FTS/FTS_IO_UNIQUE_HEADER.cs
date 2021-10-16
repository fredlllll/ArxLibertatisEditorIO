using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct FTS_IO_UNIQUE_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] path;
        internal int count;
        public float version;
        public int uncompressedsize;
        public fixed int pad[3];
    }
}
