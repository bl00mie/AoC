using System;

namespace AoC
{
    public abstract class ProgramBase
    {
        public static void Ans(object o, int part = 1) => Console.WriteLine(string.Format("Part {0}: {1}", part, o.ToString()));
        public static void Ans2(object o) => Ans(o, 2);

        public static void Log(object o) => Console.WriteLine(o.ToString());
        public static void Log(string template, params object[] os) => Console.WriteLine(string.Format(template, os));

        public static void Write(object o) => Console.Write(o.ToString());
        public static void Write(string template, params object[] os) => Console.Write(template, os);

        public static void WL() => Console.WriteLine();
        public static void WL(object o) => Console.WriteLine(o.ToString());
        public static void WL(string template, params object[] os) => Console.Write(template, os);
    }
}
