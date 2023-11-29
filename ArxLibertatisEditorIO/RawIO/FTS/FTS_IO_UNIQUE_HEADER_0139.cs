using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_UNIQUE_HEADER_0139
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] path;
        internal int count;
        public float version;

        public override readonly string ToString()
        {
            return $"path: {path}\n" +
                $"count: {count}\n" +
                $"version: {version}";
        }
    }
}
