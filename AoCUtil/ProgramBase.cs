using System;

namespace AoC
{
    public class ProgramBase
    {
        public static void Log(object o) { Console.WriteLine(o.ToString()); }
        public static void Ans(object o, int part = 1) { Console.WriteLine(string.Format("Part {0}: {1}", part, o.ToString())); }
    }
}
