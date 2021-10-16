using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    /// <summary>
    /// what is this even used for??
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DLF_IO_VLIGHTING
    {
        public int r;
        public int g;
        public int b;
    }
}
