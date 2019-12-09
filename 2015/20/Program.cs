using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2015._20
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input

            int input = 29000000;

            #endregion

            #region Part 1

            int[] presents = new int[input / 10];
            int house = FirstHouse(presents, input, 10);
            Ans(string.Format("{0} presents at house#{1}", presents[house], house));

            #endregion Part 1

            #region Part 2

            presents = new int[input / 10];
            house = FirstHouse(presents, input, 11, 50);
            Ans(string.Format("{0} presents at house#{1}", presents[house], house), 2);

            #endregion
        }

        static int FirstHouse(int[] presents, int target, int pm, int lazy=-1)
        {
            foreach (int elf in Enumerable.Range(1, presents.Length))
            {
                int pos = elf;
                int pph = pm * elf;
                int limit = lazy > 0 ? lazy : presents.Length;
                int step = 0;
                while (pos < presents.Length && step++ < limit) 
                {
                    presents[pos] += pph;
                    pos += elf;
                }
            }
            for (int i = 0; i < presents.Length; i++)
                if (presents[i] >= target)
                    return i;
            return -1;
        }
    }
}

