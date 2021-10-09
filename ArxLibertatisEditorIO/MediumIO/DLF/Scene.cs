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

        internal void WriteTo(DLF_IO_SCENE scene)
        {
            scene.name = IOHelper.GetBytes(name, 512);

            //fixed fields
            scene.pad = new int[16];
            scene.fpad = new float[16];
        }
    }
}
