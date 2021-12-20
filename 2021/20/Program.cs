using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RegExtract;

namespace AoC._2021._20
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GroupInput(2021, 20);
            var alg = input.First().First();
            AoCDictionary<Coord, char> grid = new AoCDictionary<Coord, char>('.');
            var img = input.Last().ToList();
            for (int y = 0; y < img.Count; y++)
                for (int x = 0; x < img[0].Length; x++)
                    grid[new(x, y)] = img[y][x];
            var L = 0;
            var R = img[0].Length;
            var T = 0;
            var B = img.Count;
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            //draw(grid, L, R, T, B);
            #region Part 1
            for (int i = 0; i < 50; i++)
            {
                L--;
                R++;
                T--;
                B++;
                var nextGrid = new AoCDictionary<Coord, char>(grid.def == '.' ? '#' : '.');
                for (int y = T; y < B; y++)
                    for (int x = L; x < R; x++)
                    {
                        int index = 0;
                        for (int dy = -1; dy < 2; dy++)
                            for (int dx = -1; dx < 2; dx++)
                            {
                                index <<= 1;
                                index += grid[new(x + dx, y + dy)] == '.' ? 0 : 1;
                            }
                        nextGrid[new(x,y)] = alg[index];
                    }
                grid = nextGrid;
                if (i == 1)
                    Ans(grid.Where(entry => entry.Value == '#').Count());
            }
            Ans2(grid.Where(entry => entry.Value == '#').Count());

            #endregion Part 1

            #region Part 2

            //Ans("", 2);
            #endregion
        }

        static void draw(Dictionary<Coord, char> grid, int l, int r, int t, int b)
        {
            var sb = new StringBuilder();
            for (int y = t; y < b; y++)
            {
                for (int x = l; x < r; x++)
                    sb.Append(grid[new(x, y)]);
                sb.AppendLine();
            }
            WL(sb);
        }
    }
}

