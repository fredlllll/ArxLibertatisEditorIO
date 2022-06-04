using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    public struct THEA_KEYFRAME_2014
    {
		public int num_frame;
		public int flag_frame;
		public bool master_key_frame;
		public bool key_frame;
		public bool key_move;
		public bool key_orient;
		public bool key_morph;
		public int time_frame;

		public static explicit operator THEA_KEYFRAME_2015(THEA_KEYFRAME_2014 self)
        {
			return new THEA_KEYFRAME_2015()
			{
				num_frame = self.num_frame,
				flag_frame = self.flag_frame,
				master_key_frame = self.master_key_frame,
				key_frame = self.key_frame,
				key_move = self.key_move,
				key_orient = self.key_orient,
				key_morph = self.key_morph,
				time_frame = self.time_frame,
				info_frame = new byte[256] //empty info
			};
        }
	}
}
