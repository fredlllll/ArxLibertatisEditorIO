using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Path
    {
        public readonly PathHeader pathHeader = new PathHeader();
        public readonly List<Pathway> pathways = new List<Pathway>();
        internal void ReadFrom(DLF_IO_PATH path)
        {
            pathHeader.ReadFrom(path.header);

            pathways.Clear();
            for (int i = 0; i < path.header.numPathways; ++i)
            {
                var pathway = new Pathway();
                pathway.ReadFrom(path.paths[i]);
                pathways.Add(pathway);
            }
        }

        internal void WriteTo(DLF_IO_PATH path)
        {
            pathHeader.WriteTo(path.header);

            IOHelper.EnsureArraySize(ref path.paths, pathways.Count);
            path.header.numPathways = pathways.Count;
            for (int i = 0; i < pathways.Count; ++i)
            {
                pathways[i].WriteTo(path.paths[i]);
            }
        }
    }
}
