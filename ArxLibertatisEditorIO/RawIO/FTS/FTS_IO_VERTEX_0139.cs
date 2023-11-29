using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTS_IO_VERTEX_0139
    {
        public float posY; //1
        public short posX; //100
        public short posZ; //100
        public short texU; //1024
        public short texV; //1024

        public override readonly string ToString()
        {
            return $"posY: {posY}\nposX: {posX / 10f}\nposZ: {posZ / 10f}\n" +
                $"texU: {texU / 1024f}\ntexV: {texV / 1024f}";
        }

        public readonly void WriteTo(ref FTS_IO_VERTEX ov, int x, int z)
        {
            ov.posX = x + posX / 100f;
            ov.posY = posY;
            ov.posZ = z + posZ / 100f;
            ov.texU = texU / 1024f;
            ov.texV = texV / 1024f;
        }
    }
}
