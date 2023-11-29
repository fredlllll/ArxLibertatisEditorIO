using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_UNIQUE_HEADER2
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] path;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] check;
    }
}
