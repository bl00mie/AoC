using System.Linq;
using System.Diagnostics;
using AoCUtil;
using System.Collections.Generic;

namespace AoC
{
    public abstract class ProgramBase
    {
        public static void Ans(string s, int part = 1) {
            Debug.WriteLine(string.Format("Part {0}: {1}", part, s.ToString()));
            Clipboard.Copy(s.ToString());
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
