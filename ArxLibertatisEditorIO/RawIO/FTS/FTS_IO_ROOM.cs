using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    public class FTS_IO_ROOM
    {
        public EERIE_IO_ROOM_DATA data;
        public int[] portals;
        public FTS_IO_EP_DATA[] polygons;

        public void ReadFrom(StructReader reader)
        {
            data = reader.ReadStruct<EERIE_IO_ROOM_DATA>();

            portals = new int[data.nb_portals];
            for (int i = 0; i < data.nb_portals; i++)
            {
                portals[i] = reader.ReadInt32();
            }

            polygons = new FTS_IO_EP_DATA[data.nb_polys];
            for (int i = 0; i < data.nb_polys; i++)
            {
                polygons[i] = reader.ReadStruct<FTS_IO_EP_DATA>();
            }
        }

        public void WriteTo(StructWriter writer)
        {
            data.nb_polys = CalcNumPolygons();
            data.nb_portals = CalcNumPortals();
            writer.WriteStruct(data);

            for (int i = 0; i < data.nb_portals; i++)
            {
                writer.Write(portals[i]);
            }

            for (int i = 0; i < data.nb_polys; i++)
            {
                writer.WriteStruct(polygons[i]);
            }
        }

        public int CalcNumPortals()
        {
            return portals == null ? 0 : portals.Length;
        }

        public int CalcNumPolygons()
        {
            return polygons == null ? 0 : polygons.Length;
        }

        public int CalculateWrittenSize()
        {
            int size = 0;

            size += Marshal.SizeOf<EERIE_IO_ROOM_DATA>();
            size += sizeof(int) * CalcNumPortals();
            size += Marshal.SizeOf<FTS_IO_EP_DATA>() * CalcNumPolygons();

            return size;
        }
    }
}
