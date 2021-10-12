using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Scene
    {
        public string name;

        internal void ReadFrom(DLF_IO_SCENE scene)
        {
            name = IOHelper.GetString(scene.name);
        }

        internal void WriteTo(ref DLF_IO_SCENE scene)
        {
            scene.name = IOHelper.GetBytes(name, 512);
        }

        public override string ToString()
        {
            return "Name: " + name;
        }
    }
}
