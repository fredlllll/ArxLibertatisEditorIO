using ArxLibertatisEditorIO.RawIO.FTL;
using ArxLibertatisEditorIO.Util;
using System;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class Face
    {
        public enum FaceType : int
        {
            Flat = 0,
            Text = 1,
            DoubleSided = 2,
        }

        public class EerieFaceVertex
        {
            public Color color;
            public ushort vertexIndex;
            public float u, v;
            public short ou, ov;
            public Vector3 normal;
        }

        public FaceType faceType = FaceType.Flat;
        public readonly EerieFaceVertex[] vertices = new EerieFaceVertex[]
        {
            new EerieFaceVertex(),
            new EerieFaceVertex(),
            new EerieFaceVertex(),
        };

        public short textureContainerIndex;
        public float transval;
        public Vector3 normal;
        public float temp;

        internal void ReadFrom(EERIE_FACE_FTL face)
        {
            faceType = (FaceType)face.facetype;
            textureContainerIndex = face.texid;
            transval = face.transval;
            normal = face.norm.ToVector3();
            temp = face.temp;

            for (int i = 0; i < 3; ++i)
            {
                var vert = vertices[i];
                vert.color = IOHelper.ColorFromRGBA(face.rgb[i]);
                vert.vertexIndex = face.vid[i];
                vert.u = face.u[i];
                vert.v = face.v[i];
                vert.ou = face.ou[i];
                vert.ov = face.ov[i];
                vert.normal = face.nrmls[i].ToVector3();
            }
        }

        internal void WriteTo(ref EERIE_FACE_FTL face)
        {
            face.facetype = (int)faceType;
            face.texid = textureContainerIndex;
            face.transval = transval;
            face.norm = new RawIO.Shared.SavedVec3(normal);
            face.temp = temp;

            IOHelper.EnsureArraySize(ref face.rgb, 3);
            IOHelper.EnsureArraySize(ref face.vid, 3);
            IOHelper.EnsureArraySize(ref face.u, 3);
            IOHelper.EnsureArraySize(ref face.v, 3);
            IOHelper.EnsureArraySize(ref face.ou, 3);
            IOHelper.EnsureArraySize(ref face.ov, 3);
            IOHelper.EnsureArraySize(ref face.nrmls, 3);

            for (int i = 0; i < 3; ++i)
            {
                var vert = vertices[i];
                face.rgb[i] = IOHelper.ColorToRGBA(vert.color);
                face.vid[i] = vert.vertexIndex;
                face.u[i] = vert.u;
                face.v[i] = vert.v;
                face.ou[i] = vert.ou;
                face.ov[i] = vert.ov;
                face.nrmls[i] = new RawIO.Shared.SavedVec3(vert.normal);
            }
        }
    }
}