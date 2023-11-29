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

        public override readonly string ToString()
        {
            return $"version: {version}\n" +
                $"sizex: {sizex}\n" +
                $"sizez: {sizez}\n" +
                $"nb_textures: {nb_textures}\n" +
                $"nb_polys: {nb_polys}\n" +
                $"nb_anchors: {nb_anchors}\n" +
                $"playpos: {playerpos}\n" +
                $"Mscenepos: {Mscenepos}\n" +
                $"nb_portals: {nb_portals}\n" +
                $"nb_rooms: {nb_rooms}";
        }
    }
}
