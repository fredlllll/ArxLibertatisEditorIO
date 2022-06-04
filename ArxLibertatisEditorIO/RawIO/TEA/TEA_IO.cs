using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    public class TEA_IO
    {
        public THEA_HEADER header;
        public TEA_KEYFRAME[] keyframes;


        public void ReadFrom(Stream s)
        {
            StructReader reader = new StructReader(s, Encoding.ASCII, true);

            header = reader.ReadStruct<THEA_HEADER>();

            keyframes = new TEA_KEYFRAME[header.nb_key_frames];
            for (int i = 0; i < keyframes.Length; ++i)
            {
                var kf = keyframes[i] = new TEA_KEYFRAME(this);
                kf.ReadFrom(reader);
            }
        }

        public void WriteTo(Stream s)
        {
            StructWriter writer = new StructWriter(s, Encoding.ASCII, true);

            writer.WriteStruct(header);

            for(int i =0; i< keyframes.Length; ++i)
            {
                keyframes[i].WriteTo(writer);
            }
        }
    }
}
