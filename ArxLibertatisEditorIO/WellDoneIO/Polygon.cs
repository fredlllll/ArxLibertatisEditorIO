using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ArxLibertatisEditorIO.WellDoneIO
{
    public class Polygon
    {
        public readonly Vertex[] vertices = new Vertex[4] {
        new Vertex(),new Vertex(), new Vertex(), new Vertex()
        };

        public Vector3 norm;//TODO: could calc
        public Vector3 norm2;//TODO: could calc
        public float area;//TODO: could calc

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
    }
}
