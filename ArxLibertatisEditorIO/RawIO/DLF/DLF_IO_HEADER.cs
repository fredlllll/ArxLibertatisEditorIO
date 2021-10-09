using ArxLibertatisEditorIO.RawIO.Shared;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DLF_IO_HEADER
    {
        public float version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] identifier;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] lastUser;
        public uint time; //originally signed int, but uint gives us a few more years till this breaks
        public SavedVec3 positionEdit;
        public SavedAnglef angleEdit;
        public int numScenes;
        public int numInters;
        public int numNodes;
        public int numNodelinks;
        public int numZones;
        public int lighting;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public int[] ipad1;
        public int numLights;
        public int numFogs;

        public int numBackgroundPolys;
        public int numIgnoredPolys;
        public int numChildPolys;
        public int numPaths;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 250)]
        public int[] ipad2;
        public SavedVec3 offset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 253)]
        public float[] fpad1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4096)]
        public byte[] cpad1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public int[] ipad3;
    }
}
