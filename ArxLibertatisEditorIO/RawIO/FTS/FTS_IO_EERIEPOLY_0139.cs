using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_EERIEPOLY_0139
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public FTS_IO_VERTEX_0139[] vertices;
        public int tex;
        public SavedVec3 norm;
        public SavedVec3 norm2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public SavedVec3[] normals;
        public float transval;
        public float area;
        public PolyType type;
        public short room;
        public short paddy;

        public override readonly string ToString()
        {
            return $"vertices: {Output.ToString(vertices)}\n" +
                $"tex: {tex}\n" +
                $"norm: {norm}\n" +
                $"norm2: {norm2}\n" +
                $"normals: {Output.ToString(normals)}\n" +
                $"transval: {transval}\n" +
                $"area: {area}\n" +
                $"type: {type}\n" +
                $"room: {room}\n" +
                $"paddy: {paddy}";
        }

        public readonly void WriteTo(ref FTS_IO_EERIEPOLY op,int x,int z)
        {
            op.vertices = new FTS_IO_VERTEX[4];
            for (int i = 0; i < 4; ++i)
            {
                var ov = new FTS_IO_VERTEX();
                vertices[i].WriteTo(ref ov,x,z);
                op.vertices[i] = ov;
            }
            op.tex = tex;
            op.norm = norm;
            op.norm2 = norm2;
            op.normals = normals;
            op.transval = transval;
            op.area = area;
            op.type = type;
            op.room = room;
            op.paddy = paddy;
        }
    }
}
