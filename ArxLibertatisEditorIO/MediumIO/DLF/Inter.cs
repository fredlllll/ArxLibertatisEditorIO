using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.Util;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Inter
    {
        public string name;
        public Vector3 position;
        public Vector3 euler;
        public int identifier;
        public int flags; //apparently completely unused on level load

        public void LoadFrom(ref DLF_IO_INTER inter)
        {
            name = IOHelper.GetString(inter.name);
            position = inter.pos.ToVector3();
            euler = inter.angle.ToEuler();
            identifier = inter.ident;
            flags = inter.flags;
        }

        public void SaveTo(ref DLF_IO_INTER inter)
        {
            inter.name = IOHelper.GetBytes(name, 512);
            inter.pos = new RawIO.Shared.SavedVec3(position);
            inter.angle = new RawIO.Shared.SavedAnglef(euler);
            inter.ident = identifier;
            inter.flags = flags;
        }

        public override string ToString()
        {
            return $"Name:{name}\n" +
                $"Position:{position}\n" +
                $"Eulers:{euler}\n" +
                $"Identifier:{identifier}\n" +
                $"Flags:{flags}";
        }
    }
}
