using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;

namespace ArxLibertatisEditorIO.WellDoneIO.Saving
{
    public class Cell : MediumIO.FTS.Cell
    {
        public readonly List<Color> colors = new List<Color>();

        public Cell(int x, int z) : base(x, z) { }
    }
}
