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

            var input = AoCUtil.GetAocInput(2023, 9).Select(s => s.GetInts(' ').ToList()).ToList();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            static int derive(List<int> ints)
            {
                if (ints.All(i => i == 0)) return 0;
                return ints[^1] + derive(ints.Pairwise().Select(p => p.b - p.a).ToList());
            }

            Ans(input.Sum(derive));
            foreach (var x in input) x.Reverse();
            Ans2(input.Sum(derive));
        }
    }
}

