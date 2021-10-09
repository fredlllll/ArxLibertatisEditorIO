using ArxLibertatisEditorIO.RawIO.Shared;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_ROOM_DIST_DATA
    {
        public float distance; // -1 means use truedist
        public SavedVec3 startpos;
        public SavedVec3 endpos;
    }
}
