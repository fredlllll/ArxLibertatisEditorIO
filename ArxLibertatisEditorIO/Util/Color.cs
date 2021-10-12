namespace ArxLibertatisEditorIO.Util
{
    public struct Color
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 1;
        }

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public override string ToString()
        {
            //<-2082,7783. -309,9998. 7350,121>
            return $"<R:{r} / G:{g} / B:{b} / A:{a}>";
        }
    }
}
