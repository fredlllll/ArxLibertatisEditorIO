using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.Shared
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DANAE_IO_LIGHT
    {
        public SavedVec3 pos;
        public SavedColor rgb;
        public float fallStart;
        public float fallEnd;
        public float intensity;
        public float i;
        public SavedColor ex_flicker;
        public float ex_radius;
        public float ex_frequency;
        public float ex_size;
        public float ex_speed;
        public float ex_flaresize;
        public fixed float fpad[24];
        public ExtrasType extras;
        public fixed int ipad[31];
    }
}
