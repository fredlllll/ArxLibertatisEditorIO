using System;
using System.Numerics;

namespace ArxLibertatisEditorIO.Util
{
    public static class MathHelper
    {
        public static float AreaOfTriangle(Vector3 pt1, Vector3 pt2, Vector3 pt3)
        {
            double a = Vector3.Distance(pt1, pt2);
            double b = Vector3.Distance(pt2, pt3);
            double c = Vector3.Distance(pt3, pt1);
            double s = (a + b + c) / 2;
            return (float)Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }
    }
}
