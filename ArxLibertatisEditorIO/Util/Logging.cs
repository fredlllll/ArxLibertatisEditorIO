using System;

namespace ArxLibertatisEditorIO.Util
{
    public static class Logging
    {
        public enum LogLevel
        {
            None = 0,
            Debug = 1,
            Info = 2,
            Warning = 3,
            Error = 4,
        }

        public static LogLevel logLevel = LogLevel.Info;

        public static void LogInfo(string msg)
        {

        }

        public static void LogWarning(string msg)
        {

        }

        public static void LogError(string msg)
        {

        }
        public static void LogDebug(string msg)
        {

        }

        public static void Log(string msg, LogLevel level)
        {
            if(level >= logLevel)
            {
                Console.WriteLine(msg);
            }
        }
    }
}
