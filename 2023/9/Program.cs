using System.Collections.Generic;
using System.Linq;
using AoCUtil;

namespace AoC._2023._9
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 9)
                .Select(s =>
                    s.Split(' ')
                    .Select(int.Parse)
                    .ToList())
                .ToList();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            var ans = 0;
            var ans2 = 0;
            var stack = new Stack<List<int>>();
            foreach (var v in input)
            {
                stack.Clear();
                stack.Push(v);
                while (true)
                {
                    var vals = stack.Peek();
                    var nl = vals.Pairwise().Select(p => p.b - p.a).ToList();
                    if (!nl.Any(x => x != 0)) break;
                    stack.Push(nl);
                }
                var diff = 0;
                var ldiff = 0;
                while(stack.Count > 0)
                {
                    var vals = stack.Pop();
                    diff += vals.Last();
                    ldiff = vals[0] - ldiff;
                }
                ans += diff;
                ans2 += ldiff;
            }
            Ans(ans);
            Ans(ans2);
        }
    }
}

