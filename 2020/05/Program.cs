using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020._05
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2020, 05);
            #endregion

            #region Part 1
            var s = new HashSet<int>();
            foreach (var line in input)
            {
                var bits = line.Select(x => "FL".Contains(x) ? '0' : '1');
                int val = Convert.ToInt32(string.Join("", bits), 2);
                s.Add(val);
            }
            var ordered = s.OrderBy(x => x);

            Ans(ordered.Last());
            #endregion Part 1

            #region Part 2
            Ans(ordered.First(x => !s.Contains(x+1)) + 1 , 2);
            #endregion
        }
    }
}

