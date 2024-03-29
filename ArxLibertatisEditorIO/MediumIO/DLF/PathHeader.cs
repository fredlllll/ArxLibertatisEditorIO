﻿using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.Util;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class PathHeader
    {
        public string name;
        public short idx;
        public PathFlags flags; //TODO: enum
        public Vector3 initPos;
        public Vector3 pos;
        public Color color;
        public float farClip;
        public float reverb;
        public float ambientMaxVolume;
        public int height;
        public string ambiance;

        public void LoadFrom(ref DLF_IO_PATH_HEADER header)
        {
            name = IOHelper.GetString(header.name);
            idx = header.idx;
            flags = header.flags;
            initPos = header.initPos.ToVector3();
            pos = header.pos.ToVector3();
            color = header.rgb.ToColor();
            farClip = header.farClip;
            reverb = header.reverb;
            ambientMaxVolume = header.ambientMaxVolume;
            height = header.height;
            ambiance = IOHelper.GetString(header.ambiance);
        }

        public void WriteTo(ref DLF_IO_PATH_HEADER header)
        {
            header.name = IOHelper.GetBytes(name, 64);
            header.idx = idx;
            header.flags = flags;
            header.initPos = new RawIO.Shared.SavedVec3(initPos);
            header.pos = new RawIO.Shared.SavedVec3(pos);
            header.rgb = new RawIO.Shared.SavedColor(color);
            header.farClip = farClip;
            header.reverb = reverb;
            header.ambientMaxVolume = ambientMaxVolume;
            header.height = height;
            header.ambiance = IOHelper.GetBytes(ambiance, 128);
        }

        public override string ToString()
        {
            return $"Name:{name}\n" +
                $"Index:{idx}\n" +
                $"Flags:{flags}\n" +
                $"Init Pos:{initPos}\n" +
                $"Pos:{pos}\n" +
                $"Color:{color}\n" +
                $"Far Clip:{farClip}\n" +
                $"Reverb:{reverb}\n" +
                $"Ambient Max Volume:{ambientMaxVolume}\n" +
                $"Height:{height}\n" +
                $"Ambiance:{ambiance}";
        }
    }
}
