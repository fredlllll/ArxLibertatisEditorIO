using ArxLibertatisEditorIO.RawIO.PK;

namespace ArxLibertatisEditorIO.RawIO
{
    public static class ArxIO
    {
        public static byte[] Unpack(byte[] bytes)
        {
           return Explode.DoExplode(bytes);
        }

        public static byte[] Pack(byte[] bytes)
        {
            return Implode.DoImplode(bytes);
        }
    }
}
