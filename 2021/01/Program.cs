using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2021._01
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2021, 1).Select(s => int.Parse(s)).ToList();
            #endregion

            #region Part 1
            int prev = int.MaxValue;
            var up = 0;
            foreach (var d in input)
            {
                if (d > prev)
                    up++;
                prev = d;
            }
            Ans(up);
            #endregion Part 1

            #region Part 2
            prev = int.MaxValue;
            up = 0;
            for (int i=0; i<input.Count-2; i++)
            {
                var d = input[i] + input[i+1] + input[i+2];
                if (d > prev)
                    up++;
                prev = d;
            }    
            Ans(up, 2);
            #endregion
        }
    }
}

