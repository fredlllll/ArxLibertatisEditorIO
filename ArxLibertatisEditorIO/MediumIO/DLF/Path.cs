using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Path
    {
        public readonly PathHeader pathHeader = new PathHeader();
        public readonly List<Pathway> pathways = new List<Pathway>();
        public void LoadFrom(DLF_IO_PATH path)
        {
            pathHeader.LoadFrom(ref path.header);

            pathways.Clear();
            for (int i = 0; i < path.header.numPathways; ++i)
            {
                var pathway = new Pathway();
                pathway.LoadFrom(ref path.paths[i]);
                pathways.Add(pathway);
            }
        }

        public void SaveTo(DLF_IO_PATH path)
        {
            pathHeader.WriteTo(ref path.header);

            IOHelper.EnsureArraySize(ref path.paths, pathways.Count);
            path.header.numPathways = pathways.Count;
            for (int i = 0; i < pathways.Count; ++i)
            {
                pathways[i].WriteTo(ref path.paths[i]);
            }
        }

        public override string ToString()
        {
            return $"Path Header:\n{Output.Indent(pathHeader.ToString())}\n" +
                $"Pathways({pathways.Count}):\n{Output.ToString(pathways)}";
        }
    }
}
