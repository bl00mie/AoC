using System;
using System.Linq;

namespace AoC._2021._07
{
    class Program : ProgramBase
    {
        static void Main()
        {
            var input = AoCUtil.GetAocInput(2021, 07).First().GetLongs();
            var l = input.Min();
            var r = input.Max();
            var best = long.MaxValue;
            var best2 = long.MaxValue;
            for (long i=l; i<r; i++)
            {
                var costs = input.Select(x => Math.Abs(x - i));
                var tot = costs.Sum();
                if (tot < best)
                    best = tot;

                var tot2 = costs.Select(x => x * (x + 1) / 2).Sum();
                if (tot2 < best2)
                    best2 = tot2;
            }
            Ans(best);
            Ans2(best2);
        }
    }
}

