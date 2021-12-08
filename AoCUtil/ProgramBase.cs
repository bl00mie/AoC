using System.Linq;
using System.Diagnostics;
using AoCUtil;
using System.Collections.Generic;
using System;

namespace AoC
{
    public abstract class ProgramBase
    {
        protected static DateTime startTime = DateTime.UtcNow;

        public static void Ans(string s, int part = 1) {
            var delta = (DateTime.UtcNow - startTime).TotalMilliseconds;
            Debug.WriteLine($"Part {part}: {s} ({delta} ms)");
            Clipboard.Copy(s);
        }

        public static void Ans<T>(T answer, int part = 1) => Ans(answer.ToString(), part);
        public static void Ans<T>(IEnumerable<T> os, int part = 1) => Ans(string.Join(',', os.Select(o => o.ToString())), part);

        public static void Ans2(object answer) => Ans(answer, 2);

        public static void Write<T>(T message) => Debug.Write(message);
        public static void Write(string template, params object[] os) => Debug.Write(string.Format(template, os));

        public static void WL() => Debug.WriteLine(string.Empty);
        public static void WL<T>(T message) => Debug.WriteLine(message);
        public static void WL(string template, params object[] os) => Debug.WriteLine(string.Format(template, os));
    }
}
