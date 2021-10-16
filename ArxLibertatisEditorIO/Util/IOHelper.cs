using System;
using System.IO;
using System.Text;

namespace ArxLibertatisEditorIO.Util
{
    public static class IOHelper
    {
        /// <summary>
        /// get a string from bytes
        /// be aware this searches for first non 0 char from the end, so you could end up with 0 chars in the string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string GetString(byte[] bytes)
        {
            int strlen = 0;
            for (int i = bytes.Length - 1; i >= 0; i--)
            {
                if (bytes[i] != 0)
                {
                    strlen = i + 1;
                    break;
                }
            }
            string retval = Encoding.ASCII.GetString(bytes, 0, strlen);
            return retval;
        }

        /// <summary>
        /// get fixed length bytes of a string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="bytesLength"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string str, int bytesLength)
        {
            byte[] retval = new byte[bytesLength];
            if (str != null)
            {
                try
                {
                    Encoding.ASCII.GetBytes(str, 0, str.Length, retval, 0);
                }
                catch (ArgumentException) //not enough space in retval for the string
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(str);
                    Array.Copy(bytes, retval, bytesLength);
                }
            }
            return retval;
        }

        public static Color ColorFromBGRA(uint bgra)
        {
            byte[] bytes = BitConverter.GetBytes(bgra);
            return new Color(bytes[2] / 255f, bytes[1] / 255f, bytes[0] / 255f, bytes[3] / 255f);
        }

        public static Color ColorFromRGBA(uint rgba)
        {
            byte[] bytes = BitConverter.GetBytes(rgba);
            return new Color(bytes[0] / 255f, bytes[1] / 255f, bytes[2] / 255f, bytes[3] / 255f);
        }

        public static string ArxPathToPlatformPath(string arxPath)
        {
            string[] parts = arxPath.Split('\\');
            return Path.Combine(parts);
        }

        public static string PlatformPathToArxPath(string platformPath)
        {
            string[] parts = platformPath.Split(Path.DirectorySeparatorChar);
            return string.Join("\\", parts);
        }

        public static int XZToCellIndex(int x, int z, int sizex, int sizez)
        {
            x = Mathi.Clamp(x, 0, sizex - 1);
            z = Mathi.Clamp(z, 0, sizez - 1);

            return z * sizex + x;
        }

        public static (int x, int z) CellIndexToXZ(int index, int sizex)
        {
            int z = index / sizex;
            int x = index % sizex;
            return (x, z);
        }

        public static uint ColorToBGRA(Color color)
        {
            byte[] bytes = new byte[]
            {
                (byte)(color.b*255),
                (byte)(color.g*255),
                (byte)(color.r*255),
                (byte)(color.a*255),
            };

            return BitConverter.ToUInt32(bytes, 0);
        }

        public static uint ColorToRGBA(Color color)
        {
            byte[] bytes = new byte[]
            {
                (byte)(color.r*255),
                (byte)(color.g*255),
                (byte)(color.b*255),
                (byte)(color.a*255),
            };

            return BitConverter.ToUInt32(bytes, 0);
        }

        public static void EnsureArraySize<T>(ref T[] arr, int size, bool keepContent = false)
        {
            if (arr == null)
            {
                arr = new T[size];
            }
            else if (arr.Length != size)
            {
                if (keepContent)
                {
                    T[] tmp = new T[size];
                    size = Math.Min(size, arr.Length);
                    for (int i = 0; i < size; ++i)
                    {
                        tmp[i] = arr[i];
                    }
                    arr = tmp;
                }
                else
                {
                    arr = new T[size];
                }
            }
        }
    }
}
