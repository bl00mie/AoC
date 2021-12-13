using System.Collections.Generic;
using System.Linq;

namespace AoC._2021._11
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var grid = new Grid<int>(AoCUtil.GetAocInput(2021, 11)
                .Select(line => line.Select(c => (int)(c-'0'))));
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            long total = 0;
            for (int i = 0; true; i++)
            {
                var flashers = new List<Coord>();
                foreach (var (p, v) in grid)
                    if (++grid[p] > 9)
                        flashers.Add(p);
                while (flashers.Any())
                {
                    foreach(var flasher in flashers)
                    {
                        var neighbors = grid.Neighbors(flasher, Grid.AllDirs);
                        foreach (var (p,_) in neighbors)
                            grid[p]++;
                        grid[flasher] = int.MinValue;
                    }
                    total += flashers.Count;
                    flashers.Clear();
                    foreach (var (p, v) in grid)
                        if (v > 9)
                            flashers.Add(p);
                }
                if (i == 99)
                    Ans(total);

                if (!grid.Where(x => x.v >= 0).Any())
                {
                    Ans2(i + 1);
                    break;
                }

                foreach (var (p, v) in grid)
                    if (v < 0)
                        grid[p] = 0;

                grid.Render(", ");
            }
        }
    }
}

