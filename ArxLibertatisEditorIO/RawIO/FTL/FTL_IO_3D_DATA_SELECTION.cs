using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTL_IO_3D_DATA_SELECTION
    {
        public EERIE_SELECTIONS_FTL selection;
        public int[] selected;
    }
}
