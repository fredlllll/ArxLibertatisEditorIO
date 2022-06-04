using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    public class TEA_KEYFRAME
    {
        public TEA_IO teaIO;

        public THEA_KEYFRAME_2015 keyframe;
        public SavedVec3 keyMove;
        public TEA_ORIENT keyOrient;
        public THEA_KEYMORPH keyMorph;
        public THEA_GROUPANIM[] groups;
        public int num_sample;
        public THEA_SAMPLE sample;
        public byte[] sampleContent; //this is skipped in libertatis without comment
        public int num_sfx; //skipped too

        public TEA_KEYFRAME(TEA_IO teaIO)
        {
            this.teaIO = teaIO;
        }
        public void ReadFrom(StructReader reader)
        {
            if (teaIO.header.version >= 2015)
            {
                keyframe = reader.ReadStruct<THEA_KEYFRAME_2015>();
            }
            else
            {
                keyframe = (THEA_KEYFRAME_2015)reader.ReadStruct<THEA_KEYFRAME_2014>();
            }

            if (keyframe.key_move)
            {
                keyMove = reader.ReadStruct<SavedVec3>();
            }
            if (keyframe.key_orient)
            {
                keyOrient = reader.ReadStruct<TEA_ORIENT>();
            }
            if (keyframe.key_morph)
            {
                keyMorph = reader.ReadStruct<THEA_KEYMORPH>();
            }

            groups = new THEA_GROUPANIM[teaIO.header.nb_groups];
            for (int i = 0; i < groups.Length; ++i)
            {
                groups[i] = reader.ReadStruct<THEA_GROUPANIM>();
            }

            num_sample = reader.ReadInt32();

            if (num_sample != -1)
            {
                sample = reader.ReadStruct<THEA_SAMPLE>();
                sampleContent = reader.ReadBytes(sample.sample_size);
            }

            num_sfx = reader.ReadInt32();
        }

        public void WriteTo(StructWriter writer)
        {
            if (teaIO.header.version >= 2015)
            {
                writer.WriteStruct(keyframe);
            }
            else
            {
                writer.WriteStruct((THEA_KEYFRAME_2014)keyframe);
            }

            if (keyframe.key_move)
            {
                writer.WriteStruct(keyMove);
            }
            if (keyframe.key_orient)
            {
                writer.WriteStruct(keyOrient);
            }
            if (keyframe.key_morph)
            {
                writer.WriteStruct(keyMorph);
            }

            for (int i = 0; i < groups.Length; ++i)
            {
                writer.WriteStruct(groups[i]);
            }

            writer.Write(num_sample);

            if (num_sample != -1)
            {
                writer.WriteStruct(sample);
                writer.Write(sampleContent);
            }

            writer.Write(num_sfx);
        }
    }
}
