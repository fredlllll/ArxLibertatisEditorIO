using ArxLibertatisEditorIO.Util;
using System;
using System.IO;

namespace ArxLibertatisEditorIO.RawIO.CIN
{
    public class CIN_IO
    {
        public const int CINEMATIC_VERSION_1_75 = (1 << 16) | 75; // upper 16 bits contain major, lower 16 contain minor version
        public const int CINEMATIC_VERSION_1_76 = (1 << 16) | 76;

        public CIN_IO_HEADER header;
        public byte[] unknown_string; //variable length string
        public CIN_IO_BITMAP[] bitmaps;
        public CIN_IO_SOUND_BASE[] sounds;
        public CIN_IO_SAVEDCINEMATICTRACK savedCinematicTrack;
        public CIN_IO_KEY_75[] keyframes75;
        public CIN_IO_KEY_76[] keyframes76;

        public void ReadFrom(Stream dataStream)
        {
            using (StructReader reader = new StructReader(dataStream, System.Text.Encoding.ASCII, true))
            {
                header = reader.ReadStruct<CIN_IO_HEADER>();

                unknown_string = IOHelper.ReadVariableLengthString(reader);

                int nbitmaps = reader.ReadInt32();
                bitmaps = new CIN_IO_BITMAP[nbitmaps];
                for (int i = 0; i < bitmaps.Length; i++)
                {
                    var bmp = bitmaps[i] = new CIN_IO_BITMAP();
                    bmp.ReadFrom(reader);
                }

                int nsounds = reader.ReadInt32();
                sounds = header.version switch
                {
                    CINEMATIC_VERSION_1_75 => new CIN_IO_SOUND_75[nsounds],
                    CINEMATIC_VERSION_1_76 => new CIN_IO_SOUND_76[nsounds],
                    _ => throw new Exception("unknown version for sound: " + header.version),
                };
                for (int i = 0; i < sounds.Length; i++)
                {
                    CIN_IO_SOUND_BASE sound;
                    switch (header.version)
                    {
                        case CINEMATIC_VERSION_1_75:
                            sounds[i] = sound = new CIN_IO_SOUND_75();
                            sound.ReadFrom(reader);
                            break;
                        case CINEMATIC_VERSION_1_76:
                            sounds[i] = sound = new CIN_IO_SOUND_76();
                            sound.ReadFrom(reader);
                            break;
                    }
                }

                savedCinematicTrack = reader.ReadStruct<CIN_IO_SAVEDCINEMATICTRACK>();

                switch (header.version)
                {
                    case CINEMATIC_VERSION_1_75:
                        keyframes75 = new CIN_IO_KEY_75[savedCinematicTrack.nbkey];
                        for (int i = 0; i < savedCinematicTrack.nbkey; ++i)
                        {
                            keyframes75[i] = reader.ReadStruct<CIN_IO_KEY_75>();
                        }
                        break;
                    case CINEMATIC_VERSION_1_76:
                        keyframes76 = new CIN_IO_KEY_76[savedCinematicTrack.nbkey];
                        for (int i = 0; i < savedCinematicTrack.nbkey; ++i)
                        {
                            keyframes76[i] = reader.ReadStruct<CIN_IO_KEY_76>();
                        }
                        break;
                }
            }
        }

        public void WriteTo(Stream dataStream)
        {
            //make sure that noone changed the version willy nilly, so we dont accidentally write out an invalid file structure
            switch (header.version)
            {
                case CINEMATIC_VERSION_1_75:
                    if (!(sounds is CIN_IO_SOUND_75[]))
                    {
                        throw new Exception("sounds must be of type " + nameof(CIN_IO_SOUND_75) + "[]");
                    }
                    savedCinematicTrack.nbkey = keyframes75.Length;
                    break;
                case CINEMATIC_VERSION_1_76:
                    if (!(sounds is CIN_IO_SOUND_76[]))
                    {
                        throw new Exception("sounds must be of type " + nameof(CIN_IO_SOUND_76) + "[]");
                    }
                    savedCinematicTrack.nbkey = keyframes76.Length;
                    break;
            }

            using (StructWriter writer = new StructWriter(dataStream, System.Text.Encoding.ASCII, true))
            {
                writer.WriteStruct(header);

                writer.Write(unknown_string);

                writer.Write(bitmaps.Length);

                for (int i = 0; i < bitmaps.Length; i++)
                {
                    bitmaps[i].WriteTo(writer);
                }

                writer.Write(sounds.Length);

                for (int i = 0; i < sounds.Length; i++)
                {
                    sounds[i].WriteTo(writer);
                }

                writer.WriteStruct(savedCinematicTrack);

                switch (header.version)
                {
                    case CINEMATIC_VERSION_1_75:
                        for (int i = 0; i < keyframes75.Length; i++)
                        {
                            writer.WriteStruct(keyframes75[i]);
                        }
                        break;
                    case CINEMATIC_VERSION_1_76:
                        for (int i = 0; i < keyframes76.Length; i++)
                        {
                            writer.WriteStruct(keyframes76[i]);
                        }
                        break;
                }
            }
        }
    }
}
