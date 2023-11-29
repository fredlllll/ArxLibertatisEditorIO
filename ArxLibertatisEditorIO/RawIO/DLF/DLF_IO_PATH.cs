using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    public class DLF_IO_PATH
    {
        public DLF_IO_PATH_HEADER header;
        public DLF_IO_PATHWAYS[] paths;

        public void ReadFrom(StructReader reader)
        {
            header = reader.ReadStruct<DLF_IO_PATH_HEADER>();
            paths = new DLF_IO_PATHWAYS[header.numPathways];
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i] = reader.ReadStruct<DLF_IO_PATHWAYS>();
            }
        }

        public void WriteTo(StructWriter writer)
        {
            header.numPathways = paths.Length;
            writer.WriteStruct(header);
            for (int i = 0; i < paths.Length; i++)
            {
                writer.WriteStruct(paths[i]);
            }
        }
    }
}
