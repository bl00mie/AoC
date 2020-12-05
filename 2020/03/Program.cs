using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020._03
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            
            var input = AoCUtil.GetAocInput(2020, 03).ToList();

            #endregion

            #region Part 1

            int hit = treeverse(input, (3, 1));

            Ans(hit);
            #endregion Part 1

            #region Part 2

            long mul = 1;
            (int x, int y)[] slopes = new[]
            {
                (1,1), (3,1), (5,1), (7,1), (1,2)
            };

            foreach (var slope in slopes)
                mul *= treeverse(input, slope);

            Ans(mul, 2);
            #endregion
        }

        static int treeverse( List<String> map, (int dx, int dy) slope)
        {
            int hit = 0;
            for (int y = slope.dy, x = slope.dx; y < map.Count; y += slope.dy, x = (x + slope.dx) % map[0].Length)
                if (map[y][x] == '#') hit++;
            return hit;
        }

    }

}

