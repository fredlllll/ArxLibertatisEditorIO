using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EERIE_IO_PORTALS
    {
        public EERIE_IO_EERIEPOLY poly;
        public int room_1; // facing normal
        public int room_2;
        public short useportal;
        public short paddy;

        public override readonly string ToString()
        {
            return $"poly: {poly}\n" +
                $"room_1: {room_1}\n" +
                $"room_2: {room_2}\n" +
                $"useportal: {useportal}\n" +
                $"paddy: {paddy}";
        }
    }
}
