using ArxLibertatisEditorIO.RawIO.Shared;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_SCENE_HEADER
    {
        public float version;
        public int sizex;
        public int sizez;
        internal int nb_textures;
        public int nb_polys;
        internal int nb_anchors;
        public SavedVec3 playerpos;
        public SavedVec3 Mscenepos;
        internal int nb_portals;
        internal int nb_rooms;
    }
}
