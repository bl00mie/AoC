using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace AoC
{
    public abstract class ProgramBase
    {
        public static Stopwatch Stopwatch { get; set; } = new Stopwatch();

        public static void Ans(string s, int part = 1) {
            Stopwatch.Stop();
            var ms = Stopwatch.Elapsed.TotalMilliseconds;
            if (ms < 1000)
                Debug.WriteLine($"Part {part}: {s} ({ms} ms)");
            else
            {
                var timeStr = ms < 60_000 ? $"{ms / 1000.0}" : $"{Math.Floor(ms / 60_000)}:{(ms % 60_000) / 1000.0}";
                Debug.WriteLine($"Part {part}: {s} ({timeStr})");
            }
            Clipboard.Copy(s);
            Stopwatch.Restart();
        }

        public static void Ans<T>(T answer, int part = 1) => Ans(answer.ToString(), part);
        public static void Ans<T>(IEnumerable<T> os, int part = 1) => Ans(string.Join(',', os.Select(o => o.ToString())), part);

        public static void Ans2(object answer) => Ans(answer, 2);

        public static void Write<T>(T message) => Debug.Write(message);
        public static void Write(string template, params object[] os) => Debug.Write(string.Format(template, os));

        public static void WL() => Debug.WriteLine(string.Empty);
        public static void WL<T>(T message) => Debug.WriteLine(message);
        public static void WL(string template, params object[] os) => Debug.WriteLine(string.Format(template, os));

        public static bool OutOfBounds(Coord c, int maxX, int maxY, int minX = 0, int minY = 0)
            => c.x < minX || c.x > maxX || c.y < minY || c.y > maxY;
        public static bool OutOfBounds(int x, int y, int maxX, int maxY, int minX = 0, int minY = 0)
            => x < minX || x > maxX || y < minY || y > maxY;
    }
}
