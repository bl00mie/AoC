using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2022._1
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var elves = AoCUtil.GroupInput(2022, 1).Select(g => g.Sum(l => int.Parse(l)));
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1

            var max = int.MinValue;
            foreach (var cal in elves)
            {
                if (cal > max) max = cal;
            }

            Ans(max);
            #endregion Part 1

            #region Part 2
            
            var sorted = elves.OrderByDescending(x => x).ToList();
            Ans(sorted[0]+sorted[1]+sorted[2], 2);
            
            #endregion
        }
    }
}

