using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.AMB
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct AMB_IO_KEY
    {
        public uint flags;
        public uint start;
        public uint loop;
        public uint delayMin;
        public uint delayMax;

        public AMB_IO_KEY_SETTING volume;
        public AMB_IO_KEY_SETTING pitch;
        public AMB_IO_KEY_SETTING pan;
        public AMB_IO_KEY_SETTING x;
        public AMB_IO_KEY_SETTING y;
        public AMB_IO_KEY_SETTING z;
    }
}
