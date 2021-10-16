using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.Shared
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DANAE_IO_LIGHTINGHEADER
    {
        internal int numLights;
        public int viewMode; // unused
        public int modeLight; // unused
        public int ipad;
    }
}
