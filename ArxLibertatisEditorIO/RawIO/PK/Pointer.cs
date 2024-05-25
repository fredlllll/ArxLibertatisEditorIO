namespace ArxLibertatisEditorIO.RawIO.PK
{
    /// <summary>
    /// emulates a c like pointer to a fixed size array that can be changed with pointermath
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Pointer<T>
    {
        public T[] array;
        public int offset;

        public static explicit operator T(Pointer<T> p)
        {
            return p.array[p.offset];
        }

        public static Pointer<T> operator ++(Pointer<T> p)
        {
            var pp = p.Clone();
            p.offset++;
            return pp;
        }

        public static Pointer<T> operator --(Pointer<T> p)
        {
            var pp = p.Clone();
            p.offset--;
            return pp;
        }

        public static Pointer<T> operator +(Pointer<T> p, int other)
        {
            var pp = p.Clone();
            pp.offset += other;
            return pp;
        }

        public static Pointer<T> operator -(Pointer<T> p, int other)
        {
            var pp = p.Clone();
            pp.offset -= other;
            return pp;
        }

        public Pointer<T> Clone()
        {
            return new Pointer<T>() { array = array, offset = offset };
        }
    }
}
