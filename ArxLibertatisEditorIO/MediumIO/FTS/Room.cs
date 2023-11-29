using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Room
    {
        public readonly List<int> portals = new List<int>();
        public readonly List<RoomPolygon> polygons = new List<RoomPolygon>();

        internal void ReadFrom(FTS_IO_ROOM room)
        {
            portals.Clear();
            portals.AddRange(room.portals);

            polygons.Clear();
            for (int i = 0; i < room.polygons.Length; ++i)
            {
                var p = new RoomPolygon();
                p.ReadFrom(room.polygons[i]);
                polygons.Add(p);
            }
        }

        internal void WriteTo(FTS_IO_ROOM room)
        {
            room.data.nb_portals = portals.Count;
            room.data.nb_polys = polygons.Count;

            IOHelper.EnsureArraySize(ref room.portals, portals.Count);
            for (int i = 0; i < portals.Count; ++i)
            {
                room.portals[i] = portals[i];
            }

            IOHelper.EnsureArraySize(ref room.polygons, polygons.Count);
            for (int i = 0; i < polygons.Count; ++i)
            {
                polygons[i].WriteTo(ref room.polygons[i]);
            }
        }

        public Vector3 CalculateCenter(Fts fts)
        {
            int vertexCount = 0;
            Vector3 center = Vector3.Zero;

            if (polygons == null || polygons.Count == 0)
            {
                return Vector3.Zero;
            }

            for (int i = 0; i < polygons.Count; ++i)
            {
                var poly = polygons[i];
                var cellIndex = IOHelper.XZToCellIndex(poly.cell_x, poly.cell_z, fts.sceneHeader.sizex, fts.sceneHeader.sizez);
                var cell = fts.cells[cellIndex];
                var p = cell.polygons[poly.idx];

                var vertCount = p.VertexCount;
                for (int j = 0; j < vertCount; ++j)
                {
                    center += p.vertices[j].position;
                    ++vertexCount;
                }
            }
            return center / vertexCount;
        }

        public override string ToString()
        {
            return $"Portals({portals.Count}):\n{Output.ToString(portals)}\n" +
                $"Polygons({polygons.Count}):\n{Output.ToString(polygons)}";
        }
    }
}
