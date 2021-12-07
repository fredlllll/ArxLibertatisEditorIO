using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct EERIE_IO_EERIEPOLY
    {
        public PolyType type;
        public SavedVec3 min;
        public SavedVec3 max;
        public SavedVec3 norm;
        public SavedVec3 norm2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public SavedTextureVertex[] v;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32 * 4)]
        public byte[] unused; //TODO: apparently this does hold data, question is what kind of data...
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public SavedVec3[] nrml;
        public int tex;
        public SavedVec3 center;
        public float transval;
        public float area;
        public short room;
        public short misc;

        public override string ToString()
        {
            return $"type: {type}\n" +
                $"min: {min}\n" +
                $"max: {max}\n" +
                $"norm: {norm}\n" +
                $"norm2: {norm2}\n" +
                $"v: {Output.ToString(v)}\n" +
                $"unused: {IOHelper.GetString(unused)}\n" +
                $"nrml: {Output.ToString(nrml)}\n" +
                $"tex: {tex}\n" +
                $"center: {center}\n" +
                $"transval: {transval}\n" +
                $"area: {area}\n" +
                $"room: {room}\n" +
                $"misc: {misc}";
        }
    }
}
