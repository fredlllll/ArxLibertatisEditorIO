using ArxLibertatisEditorIO.RawIO.FTS;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class RoomPolygon
    {
        public short cell_x;
        public short cell_z;
        public short idx;

        internal void ReadFrom(FTS_IO_EP_DATA epd)
        {
            cell_x = epd.cell_x;
            cell_z = epd.cell_z;
            idx = epd.idx;
        }

        internal void WriteTo(ref FTS_IO_EP_DATA epd)
        {
            epd.cell_x = cell_x;
            epd.cell_z = cell_z;
            epd.idx = idx;
        }

        public override string ToString()
        {
            return $"Cell X: {cell_x}\n" +
                $"Cell Z: {cell_z}\n" +
                $"idx: {idx}";
        }
    }
}
