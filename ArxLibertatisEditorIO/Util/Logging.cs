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
            Log(msg, LogLevel.Info);
        }

        public static void LogWarning(string msg)
        {
            Log(msg, LogLevel.Warning);
        }

        public static void LogError(string msg)
        {
            Log(msg, LogLevel.Error);
        }
        public static void LogDebug(string msg)
        {
            Log(msg, LogLevel.Debug);
        }

        public static void Log(string msg, LogLevel level)
        {
            if (logHandler != null)
            {
                logHandler(msg, level);
            }
            else
            {
                if (level >= logLevel)
                {
                    Console.WriteLine(msg);
                }
            }
        }

        public static Action<string, LogLevel> logHandler = null;
    }
}
