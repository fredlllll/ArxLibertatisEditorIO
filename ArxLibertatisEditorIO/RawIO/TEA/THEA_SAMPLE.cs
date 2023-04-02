using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    public struct THEA_SAMPLE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] sample_name;
        public int sample_size;
    }
}
