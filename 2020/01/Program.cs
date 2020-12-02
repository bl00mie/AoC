using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020._01
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            
            var nums = AoCUtil.GetAocInput(2020, 01).Select(s => long.Parse(s)).ToList<long>();

            #endregion

            #region Part 1

            Ans(go1(nums));

            #endregion Part 1

            #region Part 2

            Ans(go2(nums), 2);
            #endregion
        }

        static long go1(List<long> nums)
        {
            foreach (var x in nums)
                foreach (var y in nums)
                    if (x + y == 2002)
                        return x * y;
            return -1;
        }

        static long go2(List<long> nums)
        {
            foreach(var x in nums)
                foreach(var y in nums)
                {
                    if (x + y > 2020) continue;
                    foreach (var z in nums)
                        if (x + y + z == 2020)
                            return x * y * z;
                }
            return -1;
        }
    }
}

