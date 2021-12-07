using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_ANCHOR_DATA
    {
        public SavedVec3 pos;
        public float radius;
        public float height;
        internal short nb_linked;
        public AnchorFlags flags;

        public override string ToString()
        {
            return $"pos: {pos}\n" +
                $"radius: {radius}\n" +
                $"height: {height}\n" +
                $"nb_linked: {nb_linked}\n" +
                $"flags: {flags}";
        }
    }
}
