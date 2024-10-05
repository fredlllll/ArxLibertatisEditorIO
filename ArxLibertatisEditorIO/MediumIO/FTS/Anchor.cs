using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Anchor
    {
        public Vector3 pos;
        public float radius;
        public float height;
        public AnchorFlags flags;
        public readonly List<int> linkedAnchors = new List<int>();

        public void LoadFrom(FTS_IO_ANCHOR anchor)
        {
            pos = anchor.data.pos.ToVector3();
            radius = anchor.data.radius;
            height = anchor.data.height;
            flags = anchor.data.flags;
            linkedAnchors.Clear();
            linkedAnchors.AddRange(anchor.linkedAnchors);
        }

        public void SaveTo(ref FTS_IO_ANCHOR anchor)
        {
            anchor ??= new FTS_IO_ANCHOR();

            anchor.data.pos = new RawIO.Shared.SavedVec3(pos);
            anchor.data.radius = radius;
            anchor.data.height = height;
            anchor.data.flags = flags;
            anchor.data.nb_linked = (short)linkedAnchors.Count;
            IOHelper.EnsureArraySize(ref anchor.linkedAnchors, linkedAnchors.Count);
            for (int i = 0; i < linkedAnchors.Count; ++i)
            {
                anchor.linkedAnchors[i] = linkedAnchors[i];
            }
        }

        public override string ToString()
        {
            return $"Pos: {pos}\n" +
                $"Radius: {radius}\n" +
                $"Height: {height}\n" +
                $"Flags: {flags}\n" +
                $"Linked Anchors({linkedAnchors.Count}):\n{Output.ToString(linkedAnchors)}";
        }
    }
}
