using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2015._24
{
    class Program : ProgramBase
    {
        static int W;
        static int bags;
        static void Main()
        {
            #region input

            var input = AoCUtil.GetAocInput(2015, 24).Select(int.Parse).Reverse().ToHashSet();

            #endregion

            #region Part 1

            W = input.Sum() / bags;
            bags = 3;
            divy(input);

            Ans("");
            #endregion Part 1

            #region Part 2

            Ans("", 2);
            #endregion
        }

        public static bool divy(ISet<int> items, int filled = 0)
        {
            int i = 4;
            bool done=false;
            while (!done)
            {

            }
        }

        public static IEnumerable<ISet<int>> comboSums(ISet<int> items, int sum, int? len)
        {

        }
    }
}

