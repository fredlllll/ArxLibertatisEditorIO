using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class PortalPolygon
    {
        public PolyType type = PolyType.QUAD; //all portal polys should have QUAD set, nothing else
        public Vector3 min;
        public Vector3 max;
        public Vector3 norm;
        public Vector3 norm2;
        public readonly PortalVertex[] vertices = new PortalVertex[4]
        {
            new PortalVertex(), new PortalVertex(), new PortalVertex(), new PortalVertex()
        };
        public byte[] unused; //TODO: apparently this does hold data, question is what kind of data... size 32*4 bytes
        public int tex;
        public Vector3 center;
        public float transval;
        public float area;
        public short room;
        public short misc;

        public void LoadFrom(ref EERIE_IO_EERIEPOLY poly)
        {
            type = poly.type;
            min = poly.min.ToVector3();
            max = poly.max.ToVector3();
            norm = poly.norm.ToVector3();
            norm2 = poly.norm2.ToVector3();
            unused = poly.unused;
            tex = poly.tex;
            center = poly.center.ToVector3();
            transval = poly.transval;
            area = poly.area;
            room = poly.room;
            misc = poly.misc;
            for (int i = 0; i < 4; ++i)
            {
                vertices[i].LoadFrom(ref poly, i);
            }
        }

        public void SaveTo(ref EERIE_IO_EERIEPOLY poly)
        {
            poly.type = type;
            poly.min = new RawIO.Shared.SavedVec3(min);
            poly.max = new RawIO.Shared.SavedVec3(max);
            poly.norm = new RawIO.Shared.SavedVec3(norm);
            poly.norm2 = new RawIO.Shared.SavedVec3(norm2);
            poly.unused = unused;
            IOHelper.EnsureArraySize(ref poly.unused, 32 * 4, true);
            poly.tex = tex;
            poly.center = new RawIO.Shared.SavedVec3(center);
            poly.transval = transval;
            poly.area = area;
            poly.room = room;
            poly.misc = misc;
        }

        public override string ToString()
        {
            return $"Type: {type}\n" +
                $"Min: {min}\n" +
                $"Max: {max}\n" +
                $"Norm: {norm}\n" +
                $"Norm2: {norm2}\n" +
                $"Vertices:\n{Output.ToString(vertices)}\n" +
                $"tex: {tex}\n" +
                $"Center: {center}\n" +
                $"Transparency Value: {transval}\n" +
                $"Area: {area}\n" +
                $"Room: {room}\n" +
                $"Misc: {misc}";
        }
    }
}
