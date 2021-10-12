using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.Util;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Fog
    {
        public Vector3 position;
        public Color color;
        public float size;
        public int special; //TODO: what does this even do?
        public float scale;
        public Vector3 move;
        public Vector3 euler;
        public float speed;
        public float rotateSpeed;
        public int tolive;
        public int blend; //TODO: enum probably? flags?
        public float frequency;

        internal void ReadFrom(DLF_IO_FOG fog)
        {
            position = fog.pos.ToVector3();
            color = fog.rgb.ToColor();
            size = fog.size;
            special = fog.special;
            scale = fog.scale;
            move = fog.move.ToVector3();
            euler = fog.angle.ToEuler();
            speed = fog.speed;
            rotateSpeed = fog.rotatespeed;
            tolive = fog.tolive;
            blend = fog.blend;
            frequency = fog.frequency;
        }

        internal void WriteTo(ref DLF_IO_FOG fog)
        {
            fog.pos = new RawIO.Shared.SavedVec3(position);
            fog.rgb = new RawIO.Shared.SavedColor(color);
            fog.size = size;
            fog.special = special;
            fog.scale = scale;
            fog.move = new RawIO.Shared.SavedVec3(move);
            fog.angle = new RawIO.Shared.SavedAnglef(euler);
            fog.speed = speed;
            fog.rotatespeed = rotateSpeed;
            fog.tolive = tolive;
            fog.blend = blend;
            fog.frequency = frequency;
        }

        public override string ToString()
        {
            return $"Position: {position}\n" +
                $"Color: {color}\n" +
                $"Size: {size}\n" +
                $"Special: {special}\n" +
                $"Scale: {scale}\n" +
                $"Move: {move}\n" +
                $"Euler: {euler}\n" +
                $"Speed: {speed}\n" +
                $"Rotate Speed: {rotateSpeed}\n" +
                $"To Live: {tolive}\n" +
                $"Blend: {blend}\n" +
                $"Frequency: {frequency}";
        }
    }
}
