using ArxLibertatisEditorIO.RawIO.Shared;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct DLF_IO_HEADER
    {
        public float version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] identifier;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] lastUser;
        public uint time; //originally signed int, but uint gives us a few more years till this breaks
        public SavedVec3 positionEdit;
        public SavedAnglef angleEdit;
        internal int numScenes;
        internal int numInters;
        public int numNodes;
        public int numNodelinks;
        public int numZones;
        public int lighting;
        public fixed int ipad1[256];
        internal int numLights;
        internal int numFogs;

        public int numBackgroundPolys; //unused in libertatis
        public int numIgnoredPolys; //unused in libertatis
        public int numChildPolys; //unused in libertatis
        internal int numPaths;
        public fixed int ipad2[250];
        public SavedVec3 offset;
        public fixed float fpad1[253];
        public fixed byte cpad1[4096];
        public fixed int ipad3[256];
    }
}
