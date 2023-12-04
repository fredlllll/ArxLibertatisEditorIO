using ArxLibertatisEditorIO.Util;
using System.IO;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.FTL
{
    public class FTL_IO
    {
        public FTL_IO_PRIMARY_HEADER header;
        public FTL_IO_SECONDARY_HEADER secondaryHeader;

        public bool has3DDataSection = false;
        public FTL_IO_3D_DATA_SECTION _3DDataSection;
        //apparently all the other sections are unused and undocumented, saving other contents of file into arrays, this will break values in secondary header if modified
        public byte[] dataTill3Ddata, dataTillFileEnd;

        public void ReadFrom(Stream stream)
        {
            using StructReader reader = new StructReader(stream, Encoding.ASCII, true);

            header = reader.ReadStruct<FTL_IO_PRIMARY_HEADER>();

            secondaryHeader = reader.ReadStruct<FTL_IO_SECONDARY_HEADER>();

            long till3Ddata = secondaryHeader.offset_3Ddata - stream.Position;
            if (till3Ddata > 0)
            {
                dataTill3Ddata = reader.ReadBytes((int)till3Ddata);
            }
            else
            {
                dataTill3Ddata = null;
            }

            if (secondaryHeader.offset_3Ddata >= 0)
            {
                stream.Position = secondaryHeader.offset_3Ddata;

                _3DDataSection = new FTL_IO_3D_DATA_SECTION();
                _3DDataSection.ReadFrom(reader);
                has3DDataSection = true;
            }
            else
            {
                Logging.LogWarning("invalid 3d offset: " + secondaryHeader.offset_3Ddata);
            }

            long tillFileEnd = stream.Length - stream.Position;
            if (tillFileEnd > 0)
            {
                dataTillFileEnd = reader.ReadBytes((int)tillFileEnd);
            }
            else
            {
                dataTillFileEnd = null;
            }
        }

        public void WriteTo(Stream stream)
        {
            using StructWriter writer = new StructWriter(stream, Encoding.ASCII, true);

            writer.WriteStruct(header);

            //dont write unused stuff back as we have no way of updating secondary header offset
            secondaryHeader.offset_clothes_data = -1;
            secondaryHeader.offset_collision_spheres = -1;
            secondaryHeader.offset_cylinder = -1;
            secondaryHeader.offset_physics_box = -1;
            secondaryHeader.offset_progressive_data = -1;

            var secondaryHeaderPosition = stream.Position;
            writer.WriteStruct(secondaryHeader); //write header with old values, updated further down

            if (has3DDataSection)
            {
                secondaryHeader.offset_3Ddata = (int)stream.Position;
                _3DDataSection.WriteTo(writer);
            }
            else
            {
                secondaryHeader.offset_3Ddata = -1;
            }

            var end = stream.Position;

            stream.Position = secondaryHeaderPosition;
            writer.WriteStruct(secondaryHeader); //update offsets
            stream.Position = end; //go back to end, in case user wants to do more with the stream
        }

        public static Stream EnsureUnpacked(Stream stream)
        {
            stream.Position = 0;

            byte[] first3 = new byte[3];
            stream.Read(first3, 0, first3.Length);
            stream.Position = 0;
            if (first3[0] == 'F' && first3[1] == 'T' && first3[2] == 'L')
            {
                //uncompressed
                return stream;
            }

            return CompressionUtil.EnsureUncompressed(stream, 0, stream.Length);
        }

        public static Stream EnsurePacked(Stream stream)
        {
            return CompressionUtil.EnsureCompressed(stream, 0, stream.Length);
        }
    }
}
