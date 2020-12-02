using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020._02
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            
            var input = AoCUtil.GetAocInput(2020, 02);

            #endregion

            #region Part 1

            int good = 0;
            int good2 = 0;
            foreach (var s in input)
            {
                var sa = s.Split(" ");
                var l = sa[1][0];
                var sa2 = sa[0].Split("-");
                var lo = int.Parse(sa2[0]);
                var hi = int.Parse(sa2[1]);

                int count = 0;
                foreach (char c in sa[2])
                    if (c == l)
                        count++;
                if (count <= hi && count >= lo) good++;

                good2 += (sa[2][lo - 1] == l) ^ (sa[2][hi - 1] == l) ? 1 : 0;
            }

            Ans(good);
            #endregion Part 1


            #region Part 2

            //Ans(good2, 2);
            #endregion
        }
    }
}

