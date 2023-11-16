using ArxLibertatisEditorIO.RawIO.AMB;
using ArxLibertatisEditorIO.Util;
using System;
using System.IO;

namespace ArxLibertatisEditorIO.RawIO.AEF
{
    public class AMB_IO
    {
        public const uint AMB_VERSION_1002 = 0x01000002;
        public const uint AMB_VERSION_1003 = 0x01000003;

        public AMB_IO_HEADER header;
        public AMB_IO_TRACK_BASE[] tracks;

        public void ReadFrom(Stream dataStream)
        {
            using StructReader reader = new StructReader(dataStream);

            header = reader.ReadStruct<AMB_IO_HEADER>();

            tracks = header.version switch
            {
                AMB_VERSION_1002 => new AMB_IO_TRACK_1002[header.nbtracks],
                AMB_VERSION_1003 => new AMB_IO_TRACK_1003[header.nbtracks],
                _ => throw new Exception("unknown version for sound: " + header.version),
            };
            for (int i = 0; i < tracks.Length; i++)
            {
                AMB_IO_TRACK_BASE track;
                switch (header.version)
                {
                    case AMB_VERSION_1002:
                        tracks[i] = track = new AMB_IO_TRACK_1002();
                        track.ReadFrom(reader);
                        break;
                    case AMB_VERSION_1003:
                        tracks[i] = track = new AMB_IO_TRACK_1003();
                        track.ReadFrom(reader);
                        break;
                }
            }
        }

        public void WriteTo(Stream dataStream)
        {
            switch (header.version)
            {
                case AMB_VERSION_1002:
                    if (!(tracks is AMB_IO_TRACK_1002[]))
                    {
                        throw new Exception("tracks must be of type " + nameof(AMB_IO_TRACK_1002) + "[]");
                    }
                    break;
                case AMB_VERSION_1003:
                    if (!(tracks is AMB_IO_TRACK_1003[]))
                    {
                        throw new Exception("tracks must be of type " + nameof(AMB_IO_TRACK_1003) + "[]");
                    }
                    break;
            }
            header.nbtracks = (uint)tracks.Length;

            using StructWriter writer = new StructWriter(dataStream);

            writer.WriteStruct(header);
            for (int i = 0; i < tracks.Length; i++)
            {
                tracks[i].WriteTo(writer);
            }
        }
    }
}
