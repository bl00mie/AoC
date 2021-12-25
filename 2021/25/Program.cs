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

            var input = AoCUtil.GetAocInput(2021, 25).Select(line => line.ToArray()).ToArray();
            var W = input[0].Length;
            var H = input.Length;
            AoCDictionary<Coord, char> grid = new('.', false);
            for (int y = 0; y < H; y++)
                for (int x = 0; x < W; x++)
                    if (input[y][x] != '.')
                        grid[new(x, y)] = input[y][x];
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            int steps = 0;
            var cucTypes = new (char dir, int dx, int dy)[] { ('>', 1, 0), ('v', 0, 1) };
            var moves = new HashSet<Coord> { new(0,0) };
            var done = false;
            for (; !done; steps++)
            {
                done = true;
                foreach (var (dir, dx, dy) in cucTypes)
                {
                    moves.Clear();
                    foreach (var (p, _) in grid.Where(cuc => cuc.Value == dir))
                        if (grid[new((p.x + dx) % W, (p.y + dy) % H)] == '.')
                            moves.Add(p);
                    if (moves.Any())
                    {
                        done = false;
                        foreach (var p in moves)
                        {
                            grid[new((p.x + dx) % W, (p.y + dy) % H)] = dir;
                            grid.Remove(p);
                        }
                    }
                }
            }
            Ans(steps);
            Ans2("Merry Christmas!");
        }
    }
}

