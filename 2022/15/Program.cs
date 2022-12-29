using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2022._15
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            List<(Coord s, Coord b)> input =
                AoCUtil.GetAocInput(2022, 15)
                .Extract<(Coord a, Coord b)>(@"Sensor at x=((-?\d+), y=(-?\d+)): closest beacon is at x=((-?\d+), y=(-?\d+))")
                .ToList();

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1

            var row = 2000000;
            //var row = 10;

            var lines = new List<(int a, int b)>();
            input.Select(i => (i.s.x, dx: AoCUtil.ManhattanDistance(i.s, i.b) - Math.Abs(i.s.y - row)))
                .Where(r => r.dx >= 0)
                .Select(r => (a: r.x - r.dx, b: r.x + r.dx))
                .OrderBy(r => r.a)
                .ToList()
                .ForEach(r => {
                    if (!lines.Any() || r.a > lines[^1].b + 1)
                        lines.Add(r);
                    else
                        lines[^1] = (lines[^1].a, Math.Max(r.b, lines[^1].b));
                });
            var inlineBeaconX = input
                .Where(i => i.b.y == row)
                .Select(i => i.b.x)
                .ToArray();

            Ans(lines.Sum(l => l.b - l.a + 1 - inlineBeaconX.Count(bx => l.a <= bx && bx <= l.b)) + 1);
            #endregion Part 1

            #region Part 2
            long part2()
            {
                foreach (var (s, b) in input)
                {
                    var md = AoCUtil.ManhattanDistance(s, b);
                    for (var dx = 0; dx <= md + 1; dx++)
                    {
                        var dy = md + 1 - dx;
                        foreach (var v in GridVector.DirsDiag)
                        {
                            var p = s + v * md;
                            if (OutOfBounds(p, 4_000_000, 4_000_000))
                                continue;
                            if (valid(p))
                                return p.x * 4_000_000L + p.y;
                        }
                    }
                }
                return 0;
            }

            bool valid(Coord p)
            {
                foreach (var (s, b) in input)
                {
                    var md = AoCUtil.ManhattanDistance(s, b);
                    var d = Math.Abs(p.x - s.x) + Math.Abs(p.y - s.y);
                    if (d <= md)
                        return false;
                }
                return true;
            }

            Ans2(part2());
            #endregion
        }
    }
}

