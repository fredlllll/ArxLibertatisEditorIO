using ArxLibertatisEditorIO.MediumIO.LLF;
using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Polygon
    {
        public readonly Vertex[] vertices = new Vertex[4] {
        new Vertex(),new Vertex(), new Vertex(), new Vertex()
        };

        public Vector3 norm;//TODO: could calc
        public Vector3 norm2;//TODO: could calc
        public float area;//TODO: could calc
        public short room;  //TODO: replace with room instance?
        public PolyType polyType;
        public string texturePath;
        public float transVal;

        public int VertexCount
        {
            get { return polyType.HasFlag(PolyType.QUAD) ? 4 : 3; }
        }

        public bool IsQuad
        {
            get { return polyType.HasFlag(PolyType.QUAD); }
        }

        public bool IsTriangle
        {
            get { return !IsQuad; }
        }

        public void ReadFrom(Fts fts, FTS_IO_EERIEPOLY polygon)
        {
            for (int i = 0; i < 4; ++i)
            {
                vertices[i].ReadFrom(polygon, i);
            }
            norm = polygon.norm.ToVector3();
            norm2 = polygon.norm2.ToVector3();
            area = polygon.area;
            room = polygon.room;
            polyType = polygon.type;
            transVal = polygon.transval;
            if (!fts.tcToPath.TryGetValue(polygon.tex, out texturePath))
            {
                texturePath = "";
            }
        }

        public void WriteTo(Fts fts, ref FTS_IO_EERIEPOLY polygon)
        {
            IOHelper.EnsureArraySize(ref polygon.vertices, 4);
            IOHelper.EnsureArraySize(ref polygon.normals, 4);
            for (int i = 0; i < 4; ++i)
            {
                vertices[i].WriteTo(ref polygon, i);
            }
            polygon.norm = new RawIO.Shared.SavedVec3(norm);
            polygon.norm2 = new RawIO.Shared.SavedVec3(norm2);
            polygon.area = area;
            polygon.room = room;
            polygon.type = polyType;
            polygon.transval = transVal;

            if (!fts.pathToTc.TryGetValue(texturePath, out polygon.tex))
            {
                polygon.tex = -1;
            }

            polygon.paddy = 0;
        }

        public override string ToString()
        {
            return $"Vertices:\n{Output.ToString(vertices)}\n" +
                $"Normal : {norm}\n" +
                $"Normal 2: {norm2}\n" +
                $"Area: {area}\n" +
                $"Room: {room}\n" +
                $"Poly Type: {polyType}\n" +
                $"Texture Path: {texturePath}\n" +
                $"Transparency Value: {transVal}";
        }
    }
}
