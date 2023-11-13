using ArxLibertatisEditorIO.RawIO.Shared;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.CIN
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct CIN_IO_CINEMATICLIGHT
    {
        public SavedVec3 pos;
        public float fallin;
        public float fallout;
        public SavedColor color;
        public float intensity;
        public float intensiternd;
        public int prev; //ignored
        public int next; //ignored
    }
}
