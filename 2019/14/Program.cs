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
            Dictionary<string, long> surplus = new Dictionary<string, long>();

            void reset() { surplus = new Dictionary<string, long>(); foreach (var formula in formulae) surplus[formula.Key] = 0; }
            reset();

            long make(string target, long needed)
            {
                if (surplus[target] >= needed)
                {
                    surplus[target] -= needed;
                    return 0;
                }
                var (outputCount, requirements) = formulae[target];
                var times = (long)Math.Ceiling((needed - surplus[target]) * 1.0 / outputCount);
                surplus[target] += times * outputCount - needed;

                long tot = 0;
                foreach (var (count, name) in requirements)
                    if (name == "ORE") tot += times * count;
                    else
                        tot += make(name, times * count);
                return tot;
            }
            Ans(make("FUEL", 1));
            #endregion Part 1

            #region Part 2
            var targetOre = 1000000000000L;
            var lo = 0L;
            var hi = (long)4e6;
            while (lo < hi)
            {
                reset();
                long mid = (lo + hi) / 2;
                long ore = make("FUEL", mid);
                Log("{0} fuel uses {1} ore", mid, ore);
                if (ore > targetOre)
                    hi = mid-1;
                else if (ore < targetOre)
                    lo = mid+1;
            }
            Ans2(lo);
            #endregion
        }
    }
}

