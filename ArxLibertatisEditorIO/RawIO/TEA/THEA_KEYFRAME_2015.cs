using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    public struct THEA_KEYFRAME_2015
    {
        public int num_frame;
        public int flag_frame; //TODO: enum, -1 ??? , 9 step sound
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] info_frame;
        public bool master_key_frame;
        public bool key_frame;
        public bool key_move;
        public bool key_orient;
        public bool key_morph;
        public int time_frame;

        public static explicit operator THEA_KEYFRAME_2014(THEA_KEYFRAME_2015 self)
        {
            return new THEA_KEYFRAME_2014()
            {
                num_frame = self.num_frame,
                flag_frame = self.flag_frame,
                master_key_frame = self.master_key_frame,
                key_frame = self.key_frame,
                key_move = self.key_move,
                key_orient = self.key_orient,
                key_morph = self.key_morph,
                time_frame = self.time_frame
            };
        }
    }
}
