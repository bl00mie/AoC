using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2023._6
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 6)
                .Select(l => 
                    l.Split(":")[1]
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries));
            var races = new List<(long t, long d)>();
            for (int i = 0; i < input.First().Length; i++)
                races.Add((long.Parse(input.First()[i]), long.Parse(input.Last()[i])));

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var ans = 1L;
            foreach (var r in races)
                ans *= Winners(r.t, r.d);
            Ans(ans);
            #endregion Part 1

            #region Part 2
            var t = long.Parse(string.Join("", input.First()));
            var d = long.Parse(string.Join("", input.Last()));
            Ans2(Winners(t,d));
            #endregion
        }

        static long Winners(long t, long d)
        {
            long count = 0;
            for (int i = 1; i < t; i++)
                if (i * (t - i) > d) count++;
            return count;
        }
    }
}

