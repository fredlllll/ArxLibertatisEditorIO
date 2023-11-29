﻿using System.Collections.Generic;
using System.Linq;

namespace ArxLibertatisEditorIO.Util
{
    public static class Output
    {
        public static string Indent(string str, int spaces = 2)
        {
            string rep = "\n";
            string start = "";
            for (int i = 0; i < spaces; ++i)
            {
                rep += " ";
                start += " ";
            }
            return start + str.Replace("\n", rep);
        }

        public static string ToString<T>(IEnumerable<T> array, int indentCount = 2, int limit = 15)
        {
            int count = array.Count();
            if (count == 0)
            {
                return "[]";
            }
            string retval = "[\n";
            retval += string.Join("\n,\n", array.Take(limit).Select((x) => Indent(x.ToString(), indentCount)));
            if (count > limit)
            {
                retval += $"\n,\n... {count - limit} truncated elements";
            }
            retval += "\n]";

            return retval;
        }
    }
}
