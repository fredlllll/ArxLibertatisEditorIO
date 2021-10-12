using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class PortalVertex
    {
        public Vector3 pos;
        public float rhw;
        public Color color;
        public Color specular;
        public float tu;
        public float tv;
        public Vector3 normal;

        internal void ReadFrom(EERIE_IO_EERIEPOLY poly, int index)
        {
            var vert = poly.v[index];
            pos = vert.pos.ToVector3();
            rhw = vert.rhw;
            color = IOHelper.ColorFromRGBA(vert.color);
            specular = IOHelper.ColorFromRGBA(vert.specular);
            tu = vert.tu;
            tv = vert.tv;
            normal = poly.nrml[index].ToVector3();
        }

        internal void WriteTo(ref EERIE_IO_EERIEPOLY poly, int index)
        {
            poly.v[index].pos = new RawIO.Shared.SavedVec3(pos);
            poly.v[index].rhw = rhw;
            poly.v[index].color = IOHelper.ColorToRGBA(color);
            poly.v[index].specular = IOHelper.ColorToRGBA(specular);
            poly.v[index].tu = tu;
            poly.v[index].tv = tv;
            poly.nrml[index] = new RawIO.Shared.SavedVec3(normal);
        }

        public override string ToString()
        {
            return $"Position: {pos}\n" +
                $"rhw : {rhw}\n" +
                $"Color: {color}\n" +
                $"Specular: {specular}\n" +
                $"U: {tu}\n" +
                $"V: {tv}\n" +
                $"Normal: {normal}";
        }
    }
}
