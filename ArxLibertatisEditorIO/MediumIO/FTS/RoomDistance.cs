using ArxLibertatisEditorIO.RawIO.FTS;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class RoomDistance
    {
        public float distance = -1; // -1 means use truedist, whatever that is
        public Vector3 startpos;
        public Vector3 endpos;

        internal void ReadFrom(FTS_IO_ROOM_DIST_DATA rdd)
        {
            distance = rdd.distance;
            startpos = rdd.startpos.ToVector3();
            endpos = rdd.endpos.ToVector3();
        }

        internal void WriteTo(ref FTS_IO_ROOM_DIST_DATA rdd)
        {
            rdd.distance = distance;
            rdd.startpos = new RawIO.Shared.SavedVec3(startpos);
            rdd.endpos = new RawIO.Shared.SavedVec3(endpos);
        }

        public override string ToString()
        {
            return $"Distance: {distance}\n" +
                $"Startpos: {startpos}\n" +
                $"Endpos: {endpos}";
        }
    }
}
