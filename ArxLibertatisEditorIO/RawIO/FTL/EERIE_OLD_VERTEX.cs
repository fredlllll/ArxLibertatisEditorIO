using ArxLibertatisEditorIO.RawIO.Shared;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTL
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct EERIE_OLD_VERTEX
    {
        public fixed byte unused[32];
        public SavedVec3 vert;
        public SavedVec3 norm;
    }
}
