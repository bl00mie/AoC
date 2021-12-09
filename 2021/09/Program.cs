using System.Collections.Generic;
using System.Linq;

namespace AoC._2021._09
{
    class Program : ProgramBase
    {
        static int H => grid.Length;
        static int W => grid[0].Length;
        static byte[][] grid;
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            grid = AoCUtil.GetAocInput(2021, 9)
                .Select(line => line.ToCharArray().Select(c => (byte)(c - '0')).ToArray())
                .ToArray();
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var risk = 0;
            for (int x = 0; x < W; x++)
                for (int y = 0; y < H; y++)
                    risk += Risk(x, y);
            Ans(risk);
            #endregion Part 1

            #region Part 2
            var basins = new List<int>();
            for (int x = 0; x < W; x++)
                for (int y = 0;y < H; y++)
                    if (Risk(x, y) != 0)
                        basins.Add(Basin(x, y));
            basins.Sort();
            Ans(basins[^1] * basins[^2] * basins[^3], 2);
            #endregion
        }

        static int Risk(int x, int y)
        {
            var sz = grid[y][x];
            if ((y != 0 && grid[y - 1][x] <= sz) || (y < H - 1 && grid[y + 1][x] <= sz)
                || (x != 0 && grid[y][x - 1] <= sz) || (x < W - 1 && grid[y][x+1] <= sz))
                return 0;
            return sz + 1;
        }

        static int Basin(int x, int y)
        {
            var basinNeighbors = new HashSet<(int x, int y)> { (x, y) };

            var newNeighbors = BasinNeighbor(x, y).Except(basinNeighbors).ToHashSet();
            while (newNeighbors.Any())
            {
                var newlist = newNeighbors.ToList();
                newNeighbors.Clear();
                newlist.ForEach(n =>
                {
                    basinNeighbors.Add((n.x, n.y));
                    BasinNeighbor(n.x, n.y).ForEach(nn => newNeighbors.Add(nn));
                });
                newNeighbors = newNeighbors.Except(basinNeighbors).ToHashSet();
            }

            return basinNeighbors.Count();
        }

        static List<(int x, int y)> BasinNeighbor(int x, int y)
        {
            var val = grid[y][x];
            var neighbors = new List<(int x, int y)>();
            if (y != 0 && grid[y - 1][x] != 9 && grid[y - 1][x] > val)
                neighbors.Add((x, y-1));
            if (y < H - 1 && grid[y + 1][x] != 9 && grid[y + 1][x] > val)
                neighbors.Add((x, y + 1));
            if (x != 0 && grid[y][x-1] != 9 && grid[y][x - 1] > val)
                neighbors.Add((x - 1, y));
            if (x < W - 1 && grid[y][x+1] != 9 && grid[y][x + 1] > val)
                neighbors.Add((x + 1, y));
            return neighbors;
        }
    }
}

