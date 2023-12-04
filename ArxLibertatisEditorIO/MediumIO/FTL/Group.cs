using ArxLibertatisEditorIO.RawIO.FTL;
using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class Group
    {
        public string name;
        public int origin;
        public int indexes;
        public float blobShadowSize;
        public int[] indices;
        public void LoadFrom(ref FTL_IO_3D_DATA_GROUP group)
        {
            name = IOHelper.GetStringSafe(group.group.name);
            origin = group.group.origin;
            indexes = group.group.indexes;
            blobShadowSize = group.group.siz;
            indices = group.indices;
        }

        public void SaveTo(ref FTL_IO_3D_DATA_GROUP group)
        {
            group.group.name = IOHelper.GetBytes(name, 256);
            group.group.origin = origin;
            group.group.indexes = indexes;
            group.group.siz = blobShadowSize;
            group.group.nb_index = indices.Length;
            group.indices = indices;
        }
    }
}