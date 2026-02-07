using System;
using System.Diagnostics;

namespace ESP32Display
{
    public static class Console
    {
        //TODO: Logging level
        public static void WriteLine(string str)
        {
            if (Configuration.DebugConsole) Debug.WriteLine(DateTime.UtcNow.ToString(Configuration.LoggingTimeFormat) + " " + str);
            return;
        }

        public static void WriteException(Exception e)
        {
            WriteLine(e.ToString());
            if (!string.IsNullOrEmpty(e.Message)) WriteLine(e.Message);
            if (!string.IsNullOrEmpty(e.StackTrace)) WriteLine(e.StackTrace);
            if (e.InnerException is not null) WriteException(e.InnerException);
        }
    }
}
