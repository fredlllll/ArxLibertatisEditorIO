using ArxLibertatisEditorIO.RawIO.FTL;
using System;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class Vertex : IEquatable<Vertex>
    {
        public Vector3 vertex;
        public Vector3 normal;

        public bool Equals(Vertex other)
        {
            if(other == null)
            {
                return false;
            }
            return vertex.Equals(other.vertex) && normal.Equals(other.normal);
        }

        public override bool Equals(object obj) => Equals(obj as Vertex);
        public override int GetHashCode()
        {
            return HashCode.Combine(vertex,normal);
        }

        internal void ReadFrom(EERIE_OLD_VERTEX eERIE_OLD_VERTEX)
        {
            vertex = eERIE_OLD_VERTEX.vert.ToVector3();
            normal = eERIE_OLD_VERTEX.norm.ToVector3();
        }

        internal void WriteTo(ref EERIE_OLD_VERTEX eERIE_OLD_VERTEX)
        {
            eERIE_OLD_VERTEX.vert = new RawIO.Shared.SavedVec3(vertex);
            eERIE_OLD_VERTEX.norm = new RawIO.Shared.SavedVec3(normal);
        }
    }
}