using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.AMB
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AMB_IO_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] // must be "GAMB"
        public byte[] identifier;
        public uint version;
        public uint nbtracks;
    }
}
