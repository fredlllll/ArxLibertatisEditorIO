using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_EERIEPOLY
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public FTS_IO_VERTEX[] vertices;
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
    }
}
