using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2023._4
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 4)
                .Extract<(int cn, string ws, string ms)>(@"Card ([0-9 ]+)\: ([0-9 ]+)\|([0-9 ]+)")
                .Select(x =>
                {
                    var w = x.ws.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToHashSet<int>();
                    var m = x.ms.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToHashSet<int>();
                    return (x.cn, w, m);
                })
                .ToList();
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var ans = 0;
            var matches = new Dictionary<int, int>();
            var copies = new AoCDictionary<int, int>();
            foreach (var (cn, w, m) in input)
            {
                var match = w.Intersect(m).Count();
                if (match > 0)
                    ans += (int)Math.Pow(2, match-1);
                matches[cn] = match;
                for(int i=1; i<=match; i++)
                    copies[cn + i]++;
            }
            
            Ans(ans);
            #endregion Part 1

            #region Part 2
            var ans2 = input.Count;
            while (copies.Count > 0)
            {
                ans2 += copies.Sum(x => x.Value);
                var next = new AoCDictionary<int, int>();
                foreach(var x in copies)
                {
                    var num = x.Value;
                    var cn = x.Key;
                    var match = matches[cn];
                    for (int i = 1; i <= match; i++)
                        next[cn + i]+=num;
                }
                copies = next;
            }
            Ans2(ans2);
            #endregion
        }
    }
}

