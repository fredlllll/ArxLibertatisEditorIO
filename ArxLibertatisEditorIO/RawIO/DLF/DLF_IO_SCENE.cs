using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DLF_IO_SCENE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] name;
        public fixed int pad[16];
        public fixed float fpad[16];
    }
}
