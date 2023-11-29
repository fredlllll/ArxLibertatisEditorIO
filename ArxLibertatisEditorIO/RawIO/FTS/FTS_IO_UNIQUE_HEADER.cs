using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FTS_IO_UNIQUE_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] path;
        internal int count;
        public float version;
        public int uncompressedsize;
        public fixed int pad[3];

        public override readonly string ToString()
        {
            return $"path: {path}\n" +
                $"count: {count}\n"+
                $"version: {version}\n"+
                $"uncompressed size: {uncompressedsize}\n"+
                $"pad: {pad[0]},{pad[1]},{pad[3]}";
        }
    }
}
