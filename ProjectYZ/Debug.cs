using System;
using System.IO;

namespace ProjectYZ
{
    public class Debug
    {
        private static readonly object Lock = new object();
        private static string DebugPath = "debug.txt";

        public static void AddDebugRecord(string text, bool console)
        {
            lock (Lock)
            {
                if (console) Console.WriteLine(text);
                StreamWriter streamWriter = File.AppendText(DebugPath);
                streamWriter.WriteLine(DateTime.Now + " " + DateTime.Now.Millisecond + " " + text);
                streamWriter.Close();
            }
        }

        public static void ClearLog()
        {
            File.Delete(DebugPath);
        }
    }
}
