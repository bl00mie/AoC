using System;
using System.Diagnostics;
using AoCUtil;

namespace AoC
{
    public abstract class ProgramBase
    {
        public static void Ans(object o, int part = 1) {
            Debug.WriteLine(string.Format("Part {0}: {1}", part, o.ToString()));
            Clipboard.Copy(o.ToString());
        }
        public static void Ans2(object o) => Ans(o, 2);

        public static void Log(object o) => Console.WriteLine(o.ToString());
        public static void Log(string template, params object[] os) => Debug.WriteLine(string.Format(template, os));

        public static void Write(object o) => Debug.Write(o.ToString());
        public static void Write(string template, params object[] os) => Debug.Write(string.Format(template, os));

        public static void WL() => Debug.WriteLine(string.Empty);
        public static void WL(object o) => Debug.WriteLine(o.ToString());
        public static void WL(string template, params object[] os) => Debug.Write(string.Format(template, os));
    }
}
