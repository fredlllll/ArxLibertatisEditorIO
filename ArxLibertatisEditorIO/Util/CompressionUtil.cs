using ArxLibertatisEditorIO.RawIO.PK.Explode;
using ArxLibertatisEditorIO.RawIO.PK.Implode;
using System.IO;

namespace ArxLibertatisEditorIO.Util
{
    public static class CompressionUtil
    {
        public static Stream EnsureCompressed(Stream stream, long uncompressedBytes, long compressedBytes, bool leaveOpen = false)
        {
            MemoryStream output = new MemoryStream();
            //copy skipped bytes
            if (uncompressedBytes > 0)
            {
                byte[] skippedBytes = new byte[uncompressedBytes];
                stream.Read(skippedBytes, 0, skippedBytes.Length);
                output.Write(skippedBytes, 0, skippedBytes.Length);
            }
            byte[] bytesToPack = new byte[compressedBytes];
            stream.Read(bytesToPack, 0, bytesToPack.Length);
            byte[] packed = Implode.DoImplode(bytesToPack);
            output.Write(packed, 0, packed.Length);

            output.Position = 0;
            if (!leaveOpen)
            {
                stream.Dispose();
            }
            return output;
        }

        public static Stream EnsureUncompressed(Stream stream, long uncompressedBytes, long compressedBytes, bool leaveOpen = false)
        {
            MemoryStream output = new MemoryStream();
            //copy skipped bytes
            if (uncompressedBytes > 0)
            {
                byte[] skippedBytes = new byte[uncompressedBytes];
                stream.Read(skippedBytes, 0, skippedBytes.Length);
                output.Write(skippedBytes, 0, skippedBytes.Length);
            }
            byte[] packedBytes = new byte[compressedBytes];
            stream.Read(packedBytes, 0, packedBytes.Length);
            byte[] unpacked = Explode.DoExplode(packedBytes);
            output.Write(unpacked, 0, unpacked.Length);

            output.Position = 0;
            if(!leaveOpen)
            {
                stream.Dispose();
            }
            return output;
        }
    }
}
