using System;
using System.Collections.Generic;
using System.Linq;
using AoCUtil.Collections;
using RegExtract;

namespace AoC._2022._8
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            var f = new Grid<char>(AoCUtil.GetAocInput(2022, 8));
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var ans = f.W*2 + (f.H-2)*2;
            foreach (var (p, h) in f)
            {
                if (f.IsEdge(p)) continue;
                foreach (var v in GridVector.DirsESWN)
                {
                    var n = p + v;
                    var vis = true;
                    while (f.ContainsCoord(n))
                    {
                        if (f[n] >= h)
                        {
                            vis = false;
                            break;
                        }
                        n += v;
                    }
                    if (vis)
                    {
                        ans++;
                        break;
                    }
                }
            }
            Ans(ans);
            #endregion Part 1

            #region Part 2
            var ans2 = int.MinValue;
            foreach (var (p, h) in f)
            {
                if (f.IsEdge(p)) continue;
                var score = 1;
                foreach (var v in GridVector.DirsESWN)
                {
                    var n = p + v;
                    var vscore = 1;
                    while(f[n]<h)
                    {
                        n += v;
                        if (!f.ContainsCoord(n)) break;
                        vscore++;
                    } 
                    score *= vscore;
                }
                ans2 = Math.Max(ans2, score);
            }
            Ans2(ans2);
            #endregion
        }
    }
}

