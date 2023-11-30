using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.WellDoneIO
{
    public class Vertex : MediumIO.FTS.Vertex
    {
        public Color color;

        public void ReadFrom(MediumIO.FTS.Polygon poly, int index)
        {
            var vert = poly.vertices[index];
            position = vert.position;
            uv = vert.uv;
            normal = vert.normal;
        }

        public void WriteTo(MediumIO.FTS.Polygon poly, int index)
        {
            var vert = poly.vertices[index];
            vert.position = position;
            vert.uv = uv;
            vert.normal = normal;
        }
    }
}
