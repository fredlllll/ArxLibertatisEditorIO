using ArxLibertatisEditorIO.RawIO.Shared;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.CIN
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CIN_IO_KEY_76
    {
        public int frame;
        public int numbitmap;
        public int fx;
        public short typeinterp;
        public short force;
        public SavedVec3 pos;
        public float angz;
        public int color;
        public int colord;
        public int colorf;
        public float speed;
        public CIN_IO_CINEMATICLIGHT light;
        public SavedVec3 posgrille;
        public float angzgrille;
        public float speedtrack;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] idsound;

        public static explicit operator CIN_IO_KEY_76(CIN_IO_KEY_75 self)
        {
            return new CIN_IO_KEY_76()
            {
                frame = self.frame,
                numbitmap = self.numbitmap,
                fx = self.fx,
                typeinterp = self.typeinterp,
                force = self.force,
                pos = self.pos,
                angz = self.angz,
                color = self.color,
                colord = self.colord,
                colorf = self.colorf,
                speed = self.speed,
                light = self.light,
                posgrille = self.posgrille,
                angzgrille = self.angzgrille,
                speedtrack = self.speedtrack,
                idsound = new int[] { self.idsound, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            };
        }
    }
}
