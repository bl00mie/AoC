using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoCUtil.Collections;

namespace AoC._2022._14
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2022, 14)
                .Select(line => line.Split(" -> ")
                    .Select(sa => { var xy = sa.Split(","); return new Coord(int.Parse(xy[0]), int.Parse(xy[1])); }).ToArray()).ToArray();
            var B = input.Max(line => line.Max(xy => xy.y));
            var cave = new HashSet<Coord>();

            foreach (var line in input)
                for (int i = 1; i < line.Length; i++)
                    AoCUtil.FillLine(cave, line[i], line[i - 1]);
            var cave2 = cave.ToHashSet();
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            Ans(Fill(cave, (p) => p.y == B, new Coord(500, 0)));
            #endregion Part 1

            #region Part 2
            Ans(Fill(cave2, (p) => p == new Coord(500, 0), new Coord(500, 0), B+1));
            #endregion
        }

        static int Fill(HashSet<Coord> grid, Func<Coord, bool> done, Coord o, int floor = -1, bool verbose = false)
        {
            floor = floor == -1 ? grid.Max(xy => xy.y) : floor;
            var fallVectors = new[] {
                GridVector.N,
                GridVector.NW,
                GridVector.NE
            };

            var ans = 0;
            Coord p;

            do
            {
                p = new Coord(o);
                while (true)
                {
                    var moved = false;
                    foreach (var v in fallVectors)
                        if (!grid.Contains(p + v) && p.y < floor)
                        {
                            p += v;
                            moved = true;
                            break;
                        }
                    if (!moved)
                    {
                        grid.Add(p);
                        ans++;
                        if (verbose)
                            AoCUtil.PaintGrid(grid);
                        break;
                    }
                }
            } while (!done(p));
            return ans;
        }

        static void drawr(DefaultDictionary<Coord, char> grid, int l, int r, int b)
        {
            var w = r - l;
            var sb = new StringBuilder();
            for (int y = 0; y <= b+1; y++)
            {
                var sbl = new StringBuilder();
                for (int x = l-w; x <= r+w; x++)
                    sbl.Append(grid[new(x, y)]);
                sbl.Append('\n');
                sb.Append(sbl);
            }
            WL(sb);
        }
    }
}

