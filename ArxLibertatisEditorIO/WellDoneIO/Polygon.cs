using ArxLibertatisEditorIO.Util;
using System.Numerics;

namespace ArxLibertatisEditorIO.WellDoneIO
{
    public class Polygon
    {
        public readonly Vertex[] vertices = new Vertex[4] {
        new Vertex(),new Vertex(), new Vertex(), new Vertex()
        };

        public Vector3 norm;
        public Vector3 norm2;
        public float area;

        public short room;

        //material stuff
        public PolyType polyType;
        public string texturePath;
        public float transVal;

        public int VertexCount
        {
            get { return polyType.HasFlag(PolyType.QUAD) ? 4 : 3; }
        }

        public void ReadFrom(WellDoneArxLevel wdal, MediumIO.FTS.Polygon poly)
        {
            for (int i = 0; i < 4; ++i)
            {
                vertices[i].ReadFrom(poly, i);
            }
            norm = poly.norm;
            norm2 = poly.norm2;
            area = poly.area;
            room = poly.room;
            polyType = poly.polyType;
            if (!wdal.tcToTex.TryGetValue(poly.textureContainerId, out texturePath))
            {
                texturePath = null;
            }
            transVal = poly.transVal;
        }

        public void WriteTo(WellDoneArxLevel wdal, MediumIO.FTS.Polygon poly)
        {
            for (int i = 0; i < 4; ++i)
            {
                vertices[i].WriteTo(poly, i);
            }
            poly.norm = norm;
            poly.norm2 = norm2;
            poly.area = area;
            poly.room = room;
            poly.polyType = polyType;
            if (texturePath == null)
            {
                poly.textureContainerId = 0;
            }
            else
            {
                if (!wdal.texToTc.TryGetValue(texturePath, out poly.textureContainerId))
                {
                    poly.textureContainerId = wdal.texToTc[texturePath] = wdal.texToTc.Count + 1;
                }
            }
            poly.transVal = transVal;
        }

        public void RecalculateArea()
        {
            area = MathHelper.AreaOfTriangle(vertices[0].position, vertices[1].position, vertices[2].position);
            if (polyType.HasFlag(PolyType.QUAD))
            {
                area += MathHelper.AreaOfTriangle(vertices[1].position, vertices[2].position, vertices[3].position);
            }
        }

        public void RecalculateNormals()
        {
            norm = vertices[0].normal + vertices[1].normal + vertices[2].normal;
            norm = Vector3.Normalize(norm);
            if (polyType.HasFlag(PolyType.QUAD))
            {
                norm2 = vertices[1].normal + vertices[2].normal + vertices[3].normal;
                norm2 = Vector3.Normalize(norm2);
            }
        }
    }
}
