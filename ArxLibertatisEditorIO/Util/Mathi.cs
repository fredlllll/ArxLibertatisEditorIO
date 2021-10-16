namespace ArxLibertatisEditorIO.Util
{
    public static class Mathi
    {
        public static int Clamp(int val, int min, int max)
        {
            if (val > max)
            {
                return max;
            }
            if (val < min)
            {
                return min;
            }
            return val;
        }
    }
}
