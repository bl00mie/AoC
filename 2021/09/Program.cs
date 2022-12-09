using System.Collections.Generic;
using System.Linq;

namespace AoC._2021._09
{
    class Program : ProgramBase
    {
        static Grid<byte> grid;
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            grid = new(AoCUtil.GetAocInput(2021, 9)
                .Select(line => line.ToCharArray().Select(c => (byte)(c - '0')).ToArray())
                .ToArray());
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            static bool NotHigher((Coord p, byte v) mine, (Coord p, byte v) theirs) => theirs.v <= mine.v;
            var lows = grid.Where(loc => !grid.Neighbors(loc.p, GridVector.DirsNESW, NotHigher).Any());
            Ans(lows.Sum(loc => loc.v + 1));
            #endregion Part 1

            #region Part 2
            var basins = lows.Select(loc =>
            {
                var basinNeighbors = new HashSet<Coord> { loc.p };

                var newNeighbors = BasinNeighbors(loc.p)
                    .Select(n => n.p)
                    .Except(basinNeighbors).ToHashSet();
                while (newNeighbors.Any())
                {
                    var newlist = newNeighbors.ToList();
                    newNeighbors.Clear();
                    newlist.ForEach(n =>
                    {
                        basinNeighbors.Add(n);
                        BasinNeighbors(n).ToList().ForEach(nn => newNeighbors.Add(nn.p));
                    });
                    newNeighbors = newNeighbors.Except(basinNeighbors).ToHashSet();
                }

                return basinNeighbors.Count;
            }).ToList();
            basins.Sort();
            Ans(basins[^1] * basins[^2] * basins[^3], 2);
            #endregion
        }

        static IEnumerable<(Coord p, byte v)> BasinNeighbors(Coord p)
        {
            static bool IsBasinNeighbor((Coord p, byte v) mine, (Coord p, byte v) theirs)
                => theirs.v != 9 && mine.v < theirs.v;
            return grid.Neighbors(p, GridVector.DirsNESW, IsBasinNeighbor);
        }
    }
}

