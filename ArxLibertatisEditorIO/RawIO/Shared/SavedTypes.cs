using System;
using System.Numerics;
using System.Runtime.InteropServices;
using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.RawIO.Shared
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SavedColor
    {
        public float r;
        public float g;
        public float b;

        public SavedColor(Color col)
        {
            r = col.r;
            g = col.g;
            b = col.b;
        }

        public readonly Color ToColor()
        {
            return new Color(r, g, b);
        }

        public static bool operator ==(SavedColor c1, SavedColor c2)
        {
            return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
        }

        public static bool operator !=(SavedColor c1, SavedColor c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            if (obj is SavedColor col)
            {
                return this == col;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(r, g, b);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SavedVec3
    {
        public float x;
        public float y;
        public float z;

        public SavedVec3(Vector3 vec)
        {
            x = vec.X;
            y = vec.Y;
            z = vec.Z;
        }

        public readonly Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }

        public override string ToString()
        {
            return $"x: {x}, y: {y}, z: {z}";
        }

        public static bool operator ==(SavedVec3 v1, SavedVec3 v2)
        {
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }

        public static bool operator !=(SavedVec3 v1, SavedVec3 v2)
        {
            return !(v1 == v2);
        }

        public override bool Equals(object obj)
        {
            if (obj is SavedVec3 vec)
            {
                return this == vec;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SavedAnglef
    {
        public float a;
        public float b;
        public float g;

        public SavedAnglef(Vector3 euler)
        {
            a = euler.X; //TODO: check if this is rads or degrees and if its flipped or anything
            b = euler.Y + 90;
            g = euler.Z;
        }

        public readonly Vector3 ToEuler()
        {
            return new Vector3(a, b - 90, g); //TODO: seems that rotation is handled differently depending on what type of object its used on...
        }

        public static bool operator ==(SavedAnglef a1, SavedAnglef a2)
        {
            return a1.a == a2.a && a1.b == a2.b && a1.g == a2.g;
        }

        public static bool operator !=(SavedAnglef a1, SavedAnglef a2)
        {
            return !(a1 == a2);
        }

        public override bool Equals(object obj)
        {
            if (obj is SavedAnglef angle)
            {
                return this == angle;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(a, b, g);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SavedTextureVertex
    {
        public SavedVec3 pos;
        public float rhw;
        public uint color;
        public uint specular;
        public float tu;
        public float tv;

        public override readonly string ToString()
        {
            return $"pos: {pos}\n" +
                $"rhw: {rhw}\n" +
                $"color: {color}\n" +
                $"specular: {specular}\n" +
                $"tu: {tu}\n" +
                $"tv: {tv}";
        }

        public static bool operator ==(SavedTextureVertex v1, SavedTextureVertex v2)
        {
            return v1.pos == v2.pos && v1.rhw == v2.rhw && v1.color == v2.color && v1.specular == v2.specular && v1.tu == v2.tu && v1.tv == v2.tv;
        }

        public static bool operator !=(SavedTextureVertex v1, SavedTextureVertex v2)
        {
            return !(v1 == v2);
        }

        public override bool Equals(object obj)
        {
            if (obj is SavedTextureVertex vert)
            {
                return this == vert;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(pos, rhw, color, specular, tu, tv);
        }
    }
}
