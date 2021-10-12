using ArxLibertatisEditorIO.RawIO.DLF;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Pathway
    {
        public Vector3 rpos;
        public int flag; //enum?
        public uint time; //seems to be time to get there or to stay there

        internal void ReadFrom(DLF_IO_PATHWAYS path)
        {
            rpos = path.rpos.ToVector3();
            flag = path.flag;
            time = path.time;
        }

        internal void WriteTo(ref DLF_IO_PATHWAYS path)
        {
            path.rpos = new RawIO.Shared.SavedVec3(rpos);
            path.flag = flag;
            path.time = time;
        }

        public override string ToString()
        {
            return $"RPos: {rpos}\n" +
                $"Flag: {flag}\n" +
                $"Time: {time}";
        }
    }
}
