using System;
using System.Linq;
using RegExtract;

namespace AoC._2021._17
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            //target area: x=128..160, y=-142..-88
            var i = AoCUtil.GetAocInput(2021, 17).First().Extract<(int x1, int x2, int y1, int y2)>(@"target area: x=(-?\w+)..(-?\w+), y=(-?\w+)..(-?\w+)");
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            long highest = long.MinValue;
            long count = 0;
            for (int DX = 0; DX <= Math.Max(i.x1, i.x2); DX++)
                for (int DY = Math.Min(i.y1,i.y2); DY <= 150; DY++)
                {
                    var landed = false;
                    long x = 0, y = 0;
                    int dx = DX, dy = DY;
                    long maxY = long.MinValue;
                    for (int step = 0; step < 1000 && !landed; step++)
                    {
                        x += dx; y += dy;
                        dy--;
                        if (dx > 0) dx--;
                        else if (dx < 0) dx++;
                        maxY = Math.Max(maxY, y);
                        if (x >= Math.Min(i.x1, i.x2) && x <= Math.Max(i.x1, i.x2) && y >= Math.Min(i.y1, i.y2) && y <= Math.Max(i.y1, i.y2))
                            landed = true;
                    }
                    if (landed)
                    {
                        count++;
                        highest = Math.Max(highest, maxY);
                    }
                }
            Ans(highest);
            Ans2(count);
        }
    }
}

