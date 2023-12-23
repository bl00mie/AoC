using System.Collections.Generic;
using System.Linq;
using AoCUtil.Collections;

namespace AoC._2021._15
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            var input = AoCUtil.GetAocInput(2021, 15).Select(s => s.Select(c => (int)(c - '0')).ToArray()).ToArray();
            var grid = new Grid<int>(input);
            var X = input.First().Length;
            var Y = input.Length;
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            int Solve(int mulx, int muly)
            {
                var XX = X * mulx; 
                var YY = Y * muly;
                var q = new PriorityQueue<(Coord, int), int>();
                var visited = new HashSet<Coord>();
                var risks = new DefaultDictionary<Coord, int>();
                q.Enqueue((new(0, 0), 0), 0);

                while (q.Count > 0)
                {
                    var (p, risk) = q.Dequeue();
                    if (visited.Contains(p))
                        continue;

                    visited.Add(p);
                    risks[p] = risk;

                    if (p.x == XX - 1 && p.y == YY - 1)
                        break;

                    foreach (var v in GridVector.DirsESWN)
                    {
                        var x = p.x + v.dx;
                        var y = p.y + v.dy;
                        if (OutOfBounds(x, y, XX - 1, YY - 1))
                            continue;
                        var nextRisk = risk + (grid[x % X, y % Y] + x / X + y / Y - 1) % 9 + 1;
                        q.Enqueue((new(x, y), nextRisk), nextRisk);
                    }
                }
                return risks[new(XX - 1, YY - 1)];
            }
            Ans(Solve(1,1));
            Ans2(Solve(5, 5));
        }
    }
}

