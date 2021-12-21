using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2021._21
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2021, 21);
            var P1 = input.First().Extract<int>(@"Player 1 starting position: (\w+)") - 1;
            var P2 = input.Last().Extract<int>(@"Player 2 starting position: (\w+)") - 1;
            var pos = new int[] { P1, P2 };

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var s = new int[] { 0, 0 };
            int die = 1;
            while (s[0] < 1000 && s[1] < 1000)
            {
                var delta = die++ + die++ + die++;
                var player = die % 2;
                pos[player] = (pos[player] + delta) % 10;
                s[player] += pos[player] + 1;
            }
            Ans(Math.Min(s[0], s[1]) * (die-1));
            #endregion Part 1

            #region Part 2
            var (S1, S2) = Count(P1, P2, 0, 0);
            Ans2(Math.Max(S1, S2));
            #endregion
        }

        static Dictionary<(int, int, int, int), (long, long)> history = new();
        static int[] Die = new[] { 1, 2, 3 };

        static (long, long) Count(int p1, int p2, int s1, int s2)
        {
            if (s1 >= 21) return (1, 0);
            if (s2 >= 21) return (0, 1);
            if (history.ContainsKey((p1, p2, s1, s2))) return history[(p1, p2, s1, s2)];
            var count = (0L, 0L);
            foreach (var d1 in Die)
                foreach (var d2 in Die)
                    foreach (var d3 in Die)
                    {
                        var np = (p1 + d1 + d2 + d3) % 10;
                        var (c2, c1) = Count(p2, np, s2, s1 + np + 1);
                        count = (count.Item1 + c1, count.Item2 + c2);
                    }
            history[(p1, p2, s1, s2)] = count;
            return count;
        }
    }
}

