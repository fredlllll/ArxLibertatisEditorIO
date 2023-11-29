using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.AMB
{
    public enum AMB_IO_KEY_SETTING_FLAGS : uint
    {
        RANDOM = 1,
        INTERPOLATE = 2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AMB_IO_KEY_SETTING
    {
        public float min;
        public float max;
        public uint interval; //milliseconds
        public AMB_IO_KEY_SETTING_FLAGS flags;
    }
}
