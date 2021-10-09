using ArxLibertatisEditorIO.RawIO.DLF;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Pathway
    {
        public Vector3 rpos;
        public int flag; //enum?
        public uint time; //is this unix time? or game time? why would a pathways have a unix time?

        internal void ReadFrom(DLF_IO_PATHWAYS path)
        {
            rpos = path.rpos.ToVector3();
            flag = path.flag;
            time = path.time;
        }

        internal void WriteTo(DLF_IO_PATHWAYS path)
        {
            path.rpos = new RawIO.Shared.SavedVec3(rpos);
            path.flag = flag;
            path.time = time;

            path.fpadd = new float[2];
            path.lpadd = new int[2];
            path.cpadd = new byte[32];
        }
    }
}
