using ArxLibertatisEditorIO.RawIO.FTS;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Vertex
    {
        public Vector3 position;
        public Vector2 uv;
        public Vector3 normal;

        public void LoadFrom(ref FTS_IO_EERIEPOLY poly, int index)
        {
            var vert = poly.vertices[index];
            position = new Vector3(vert.posX, vert.posY, vert.posZ);
            uv = new Vector2(vert.texU, vert.texV);
            normal = poly.normals[index].ToVector3();
        }

        public void SaveTo(ref FTS_IO_EERIEPOLY poly, int index)
        {
            poly.vertices[index].posX = position.X;
            poly.vertices[index].posY = position.Y;
            poly.vertices[index].posZ = position.Z;
            poly.vertices[index].texU = uv.X;
            poly.vertices[index].texV = uv.Y;

            poly.normals[index] = new RawIO.Shared.SavedVec3(normal);
        }

        public override string ToString()
        {
            return $"Position: {position}\n" +
                $"UV : {uv}\n" +
                $"Normal: {normal}";
        }
    }
}
