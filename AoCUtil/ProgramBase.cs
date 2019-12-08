using System;

namespace AoC
{
    public abstract class ProgramBase
    {
        public static void Ans(object o, int part = 1) => Console.WriteLine(string.Format("Part {0}: {1}", part, o.ToString()));
        public static void Log(object o) => Console.WriteLine(o.ToString());
        public static void Write(object o) => Console.Write(o.ToString());
        public static void WL() => Console.WriteLine();
        public static void WL(object o) => Console.WriteLine(o.ToString());
    }
}
