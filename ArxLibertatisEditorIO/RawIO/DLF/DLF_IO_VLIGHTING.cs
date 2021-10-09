using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DLF_IO_VLIGHTING
    {
        public int r;
        public int g;
        public int b;
    }
}
