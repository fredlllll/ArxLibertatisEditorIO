using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ArxQuat
    {
        public float w, x, y, z;
    }
}
