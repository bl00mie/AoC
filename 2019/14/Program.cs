using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2019._14
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input

            var formulae = AoCUtil.GetAocInput(2019, 14).Select(L => L.Split(" => "))
                .ToDictionary(sa => {
                    var sa1 = sa[1].Split(' ');
                    return sa1[1];
                }, sa =>
                {
                    var sa1 = sa[1].Split(' ');
                    return (outputCount:int.Parse(sa1[0]), requirements: sa[0].Split(", ").Select(ingredient =>
                   {
                       var ia = ingredient.Split(' ');
                       return (count: int.Parse(ia[0]), name: ia[1]);
                   }).ToList());
                });

            #endregion

            #region Part 1

            Dictionary<string, long> leftovers = new Dictionary<string, long>();

            void reset() { leftovers = new Dictionary<string, long>(); foreach (var formula in formulae) leftovers[formula.Key] = 0; }
            reset();

            long make(string target, long needed)
            {
                if (leftovers[target] >= needed)
                {
                    leftovers[target] -= needed;
                    return 0;
                }
                var formula = formulae[target];
                long times = (long)Math.Ceiling((needed - leftovers[target]) * 1.0 / formula.outputCount);
                leftovers[target] += times * formula.outputCount - needed;

                long tot = 0;
                foreach (var req in formula.requirements)
                {
                    if (req.name == "ORE") tot += times * req.count;
                    else
                    {
                        tot += make(req.name, times * req.count);
                    }
                }
                return tot;
            }
            Ans(make("FUEL", 1));
            #endregion Part 1

            #region Part 2

            var lo = 0L;
            var hi = (long)1e8;
            while (lo < hi)
            {
                reset();
                long mid = (lo + hi) / 2;
                long ore = make("FUEL", mid);
                Console.Write(mid);
                Console.Write("->");
                Console.WriteLine(ore);
                if (ore > 1000000000000L)
                    hi = mid - 1;
                else if (ore < 1000000000000)
                    lo = mid+1;
            }

            Ans(lo, 2);
            #endregion
        }
    }
}

