using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Cell
    {
        public readonly int x, z;

        public static Cell FromIndex(int index, int sizex)
        {
            (int x, int z) = IOHelper.CellIndexToXZ(index, sizex);
            return new Cell(x, z);
        }

        public Cell(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public readonly List<Polygon> polygons = new List<Polygon>();
        public readonly List<int> anchors = new List<int>();

        internal void ReadFrom(FTS_IO_CELL cell)
        {
            polygons.Clear();
            for (int i = 0; i < cell.polygons.Length; ++i)
            {
                var p = new Polygon();
                p.ReadFrom(cell.polygons[i]);
                polygons.Add(p);
            }

            anchors.Clear();
            anchors.AddRange(cell.anchors);
        }

        internal void WriteTo(ref FTS_IO_CELL cell)
        {
            IOHelper.EnsureArraySize(ref cell.polygons, polygons.Count);
            for (int i = 0; i < polygons.Count; ++i)
            {
                polygons[i].WriteTo(ref cell.polygons[i]);
            }

            IOHelper.EnsureArraySize(ref cell.anchors, anchors.Count);
            for (int i = 0; i < anchors.Count; ++i)
            {
                cell.anchors[i] = anchors[i];
            }
        }

        public override string ToString()
        {
            return $"x: {x}\n" +
                $"z: {z}\n" +
                $"polygons:\n{Output.ToString(polygons)}\n" +
                $"anchors:\n{Output.ToString(anchors)}\n";
        }
    }
}
