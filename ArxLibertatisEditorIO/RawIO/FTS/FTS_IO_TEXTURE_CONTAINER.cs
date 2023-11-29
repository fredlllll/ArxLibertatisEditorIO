using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_TEXTURE_CONTAINER
    {
        public int tc;
        public int temp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] fic;

        public override readonly string ToString()
        {
            return $"tc: {tc}\n" +
                $"tmp: {temp}\n" +
                $"fic: {IOHelper.GetString(fic)}";
        }
    }
}
