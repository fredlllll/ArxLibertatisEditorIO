using System;

namespace ArxLibertatisEditorIO.Util
{
    public struct Color
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public Color(float r, float g, float b, float a = 1)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public override string ToString()
        {
            return $"<R:{r} / G:{g} / B:{b} / A:{a}>";
        }

        public void Clamp()
        {
            r = Math.Clamp(r, 0, 1);
            g = Math.Clamp(g, 0, 1);
            b = Math.Clamp(b, 0, 1);
            a = Math.Clamp(a, 0, 1);
        }

        public Color Clamped()
        {
            var d = this; //value type, this creates a copy
            d.Clamp();
            return d;
        }

        public static Color operator +(Color a, float value)
        {
            return new Color(a.r + value, a.g + value, a.b + value, a.a + value);
        }

        public static Color operator +(Color a, Color value)
        {
            return new Color(a.r + value.r, a.g + value.g, a.b + value.b, a.a + value.a);
        }

        public static Color operator -(Color a, float value)
        {
            return new Color(a.r - value, a.g - value, a.b - value, a.a - value);
        }

        public static Color operator -(Color a, Color value)
        {
            return new Color(a.r - value.r, a.g - value.g, a.b - value.b, a.a - value.a);
        }

        public static Color operator *(Color a, float value)
        {
            return new Color(a.r * value, a.g * value, a.b * value, a.a * value);
        }

        public static Color operator *(Color a, Color value)
        {
            return new Color(a.r * value.r, a.g * value.g, a.b * value.b, a.a * value.a);
        }

        public static Color operator /(Color a, float value)
        {
            return new Color(a.r / value, a.g / value, a.b / value, a.a / value);
        }

        public static Color operator /(Color a, Color value)
        {
            return new Color(a.r / value.r, a.g / value.g, a.b / value.b, a.a / value.a);
        }
    }
}
