using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.LLF
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct LLF_IO_HEADER
    {
        public float version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] identifier;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] lastuser;
        public uint time; //originally signed int. same reason as in dlf header
        public int numLights;
        public int numShadowPolys;
        public int numIgnoredPolys;
        public int numBackgroundPolys;
        public fixed int ipad1[256];
        public fixed float fpad[256];
        public fixed byte cpad[4096];
        public fixed int ipad2[256];
    }
}
