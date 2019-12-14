using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2015._24
{
    class Program : ProgramBase
    {
        static int W;
        static int bags;
        static long minQE = long.MaxValue;
        static void Main()
        {
            #region input

            var input = AoCUtil.GetAocInput(2015, 24).Select(int.Parse).ToList();

            #endregion

            #region Part 1
            bags = 3;
            W = input.Sum() / bags;
            Divy(input, 0, W, 1, 0, 6);

            Ans(minQE);
            #endregion Part 1

            #region Part 2
            minQE = long.MaxValue;

            bags = 4;
            W = input.Sum() / bags;
            Divy(input, 0, W, 1, 0, 5);

            Ans2(minQE);
            #endregion
        }

        public static long Divy(List<int> items, int pos, int w, long qe, int sz, int maxSz)
        {
            if (sz > maxSz) return long.MaxValue;
            if (w == 0)
            {
                if (qe < minQE) minQE = qe;
                return qe;
            }
            else if (w < 0 || pos == items.Count)
                return long.MaxValue;

            var included = Divy(items, pos + 1, w - items[pos], qe * items[pos], sz + 1, maxSz);
            var notIncluded = Divy(items, pos + 1, w, qe, sz, maxSz);

            return Math.Min(included, notIncluded);
        }
    }
}

