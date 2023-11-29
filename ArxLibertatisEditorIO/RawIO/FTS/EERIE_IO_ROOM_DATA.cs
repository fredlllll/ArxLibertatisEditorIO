using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct EERIE_IO_ROOM_DATA
    {
        internal int nb_portals;
        internal int nb_polys;
        public fixed int padd[6];
    }
}
