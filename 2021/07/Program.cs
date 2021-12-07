using System;
using System.Linq;

namespace AoC._2021._07
{
    class Program : ProgramBase
    {
        static void Main()
        {
            var input = AoCUtil.GetAocInput(2021, 07).First().GetLongs();
            var r = input.Max();
            var best = long.MaxValue;
            var best2 = long.MaxValue;
            for (long i=input.Min(); i<r; i++)
            {
                var distances = input.Select(x => Math.Abs(x - i));
                var tot = distances.Sum();
                best = Math.Min(tot, best);

                var tot2 = distances.Select(x => x * (x + 1) / 2).Sum();
                best2 = Math.Min(best2, tot2);
            }
            Ans(best);
            Ans2(best2);
        }
    }
}

