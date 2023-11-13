using ArxLibertatisEditorIO.MediumIO.DLF;
using ArxLibertatisEditorIO.RawIO.TEA;
using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.CIN
{
    public abstract class CIN_IO_SOUND_BASE
    {
        public byte[] soundPath;

        public abstract void ReadFrom(StructReader reader);
        public abstract void WriteTo(StructWriter writer);
    }

    public class CIN_IO_SOUND_76 : CIN_IO_SOUND_BASE
    {
        public short soundId; //ignored in the game

        public override void ReadFrom(StructReader reader)
        {
            soundId = reader.ReadInt16();
            soundPath = IOHelper.ReadVariableLengthString(reader);
        }

        public override void WriteTo(StructWriter writer)
        {
            writer.Write(soundId);
            writer.Write(soundPath);
        }
    }

    public class CIN_IO_SOUND_75 : CIN_IO_SOUND_BASE
    {
        public override void ReadFrom(StructReader reader)
        {
            soundPath = IOHelper.ReadVariableLengthString(reader);
        }

        public override void WriteTo(StructWriter writer)
        {
            writer.Write(soundPath);
        }
    }
}
