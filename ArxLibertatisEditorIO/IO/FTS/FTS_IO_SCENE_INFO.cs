using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.IO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_SCENE_INFO
    {
        public int nbpoly;
        public int nbianchors;
    }
}
