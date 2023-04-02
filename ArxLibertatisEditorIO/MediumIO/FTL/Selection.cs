using ArxLibertatisEditorIO.RawIO.FTL;
using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class Selection
    {
        public string name;
        public int selected;
        public int[] selectedVertices;

        internal void ReadFrom(FTL_IO_3D_DATA_SELECTION selection)
        {
            name = IOHelper.GetString(selection.selection.name);
            selected = selection.selection.selected;
            selectedVertices = selection.selected;
        }

        internal void WriteTo(ref FTL_IO_3D_DATA_SELECTION selection)
        {
            selection.selection.name = IOHelper.GetBytes(name, 64);
            selection.selection.selected = selected;
            selection.selection.nb_selected = selectedVertices.Length;
            selection.selected = selectedVertices;
        }
    }
}