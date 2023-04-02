using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    public struct TEA_ORIENT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] theo_angle; //unused
        public ArxQuat quaternion;
    }
}
