using System;
using System.Linq;
using RegExtract;

namespace AoC._2021._05
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2021, 05)
                .Extract<(int x1,int y1,int x2,int y2)>(@"(\d+),(\d+) -> (\d+),(\d+)")
                .ToList();
            var X = input.Select(tuple => Math.Max(tuple.x1, tuple.x2)).Max();
            var Y = input.Select(tuple => Math.Max(tuple.y1, tuple.y2)).Max();
            #endregion

            var grid = new int[X+1, Y+1];
            var grid2 = new int[X+1, Y+1];
            input.ForEach(vent =>
            {
                if (vent.x1 == vent.x2)
                    for (int i=Math.Min(vent.y1, vent.y2); i <= Math.Max(vent.y1, vent.y2); i++)
                        grid[vent.x1, i]++;
                else if (vent.y1 == vent.y2)
                    for (int i = Math.Min(vent.x1, vent.x2); i <= Math.Max(vent.x1, vent.x2); i++)
                        grid[i, vent.y1]++;
                
                var dx = vent.x2 - vent.x1;
                var dy = vent.y2 - vent.y1;
                if (dx == 0 || dy == 0 || Math.Abs(dy) == Math.Abs(dy))
                {
                    var xinc = dx < 0 ? -1 : dx == 0 ? 0 : 1;
                    var yinc = dy < 0 ? -1 : dy == 0 ? 0 : 1;
                    for (int x = vent.x1, y = vent.y1; x - xinc != vent.x2 || y - yinc != vent.y2; x += xinc, y += yinc)
                    {
                        grid2[x, y]++;
                    }
                }
            });
            
            Ans(Crosses(grid, X, Y));
            Ans(Crosses(grid2, X, Y));
        }

        static int Crosses(int[,] grid, int X, int Y)
        {
            var crosses = 0;
            for (int i = 0; i <= X; i++)
                for (int j = 0; j <= Y; j++)
                    if (grid[i, j] >= 2)
                        crosses++;
            return crosses;
        }

        static void Draw(int[,] grid, int X, int Y)
        {
            for (int y = 0; y <= Y; y++)
            {
                for (int x = 0; x <= X; x++)
                    Write(grid[x, y]);
                WL();
            }
        }

    }
}

