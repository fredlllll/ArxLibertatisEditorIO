using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_SCENE_INFO
    {
        internal int nbpoly;
        internal int nbianchors;
    }
}
