using System.IO;

namespace ArxLibertatisEditorIO
{
    public static class ArxPaths
    {
        public static string DataDir
        {
            get;set;
        }

        public static string DLFDir
        {
            get
            {
                return Path.Combine(DataDir, "graph", "levels");
            }
        }

        public static string LLFDir
        {
            get
            {
                return DLFDir; //same as dlf
            }
        }

        public static string FTSDir
        {
            get
            {
                return Path.Combine(DataDir, "game", "graph", "levels");
            }
        }

        public static string GetDlfPath(string levelname)
        {
            return Path.Combine(DLFDir, levelname, levelname + ".dlf");
        }

        public static string GetLlfPath(string levelname)
        {
            return Path.Combine(LLFDir, levelname, levelname + ".llf");
        }

        public static string GetFtsPath(string levelname)
        {
            return Path.Combine(FTSDir, levelname, "fast.fts");
        }
    }
}
