using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.CIN
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CIN_IO_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] // must be "KFA\0"
        public byte[] identifier;
        public int version; //should be 1.75 or 1.76
    }
}
