using System.Collections.Generic;
using System.Linq;
using AoCUtil;
using Nito.Collections;

namespace AoC._2022._23
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var grid = AoCUtil.GetAocInput(2022, 23)
                .SelectMany((line, y) => 
                    line.Select((v, x) => (v, new Coord(x, y)))
                    .Where(t => t.v == '#')
                    .Select(t => t.Item2)
                ).ToHashSet();
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            var cones = new Deque<GridVector[]>(new []
            {
                // north is up, so swap
                new GridVector[] { GridVector.SW, GridVector.S, GridVector.SE },
                new GridVector[] { GridVector.NW, GridVector.N, GridVector.NE },
                new GridVector[] { GridVector.NW, GridVector.W, GridVector.SW },
                new GridVector[] { GridVector.NE, GridVector.E, GridVector.SE },
            });

            var i = 1;
            while(true)
            {
                var proposals = new AoCDictionary<Coord, List<Coord>>(null, false);
                foreach (var p in grid)
                    if (GridVector.DirsAll.Any(v => grid.Contains(p + v)))
                        foreach (var cone in cones)
                            if (!cone.Any(v => grid.Contains(p + v)))
                            {
                                var n = p + cone[1];
                                if (proposals[n] == null)
                                    proposals[n] = new();
                                proposals[n].Add(p);
                                break;
                            }

                var moves = proposals.Where(kvp => kvp.Value.Count == 1)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Single());
                if (!moves.Any())
                {
                    Ans2(i);
                    break;
                }
                foreach (var (np,p) in moves)
                {
                    grid.Remove(p);
                    grid.Add(np);
                }
                cones.AddToBack(cones.RemoveFromFront());

                if (i++ == 10)
                {
                    var (X, XX, Y, YY) = grid.GetRectangle();
                    Ans((XX - X + 1) * (YY - Y + 1) - grid.Count);
                }
            }
        }
    }
}

