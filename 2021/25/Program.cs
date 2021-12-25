using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2021._25
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var grid = AoCUtil.GetAocInput(2021, 25).Select(line => line.ToArray()).ToArray();
            var W = grid[0].Length;
            var H = grid.Length;

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            int steps = 0;
            var cucTypes = new (char dir, int dx, int dy)[] { ('>', 1, 0), ('v', 0, 1) };
            var done = false;
            for (; !done; steps++)
            {
                done = true;
                foreach (var (dir, dx, dy) in cucTypes)
                {
                     var moves= grid.SelectMany((row, y) => row.Select((v, x) => (x, y, v)))
                        .Where(item => item.v == dir && grid[(item.y + dy) % H][(item.x + dx) % W] == '.')
                        .ToList();
                    if (moves.Any())
                    {
                        done = false;
                        foreach (var (x, y, _) in moves)
                        {
                            grid[(y + dy) % H][(x + dx) % W] = dir;
                            grid[y][x] = '.';
                        }
                    }
                }
            }
            Ans(steps);
            Ans2("Merry Christmas!");
        }
    }
}

