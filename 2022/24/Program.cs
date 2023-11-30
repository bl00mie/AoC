using System;
using System.Collections.Generic;
using System.Linq;
using Nito.Collections;

namespace AoC._2022._24
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            Dictionary<Coord, char> grid = AoCUtil.GetAocInput(2022, 24)
                .SelectMany((line, y) =>
                    line.Select((v, x) => (new Coord(x-1, y), v))
                    .Where(t => t.v != '#')
                ).ToDictionary(t => t.Item1, t => t.v);
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            var W = grid.Count(pair => pair.Key.y == 1);
            var H = grid.Count(pair => pair.Key.x == 1);
            var S = new Coord(0, -1);
            var E = new Coord(W - 1, H);

            var bliz = new Dictionary<int, HashSet<Coord>>();
            for (int t = 0; t <= H * W; t++)
                bliz[t] = grid
                    .Where(p => p.Value != '.')
                    .Select(p => p.Value switch
                    {
                        '>' => new Coord((p.Key.x + t) % W, p.Key.y),
                        '<' => new Coord((p.Key.x - t + W) % W, p.Key.y),
                        'v' => new Coord(p.Key.x, (p.Key.y + t) % H),
                        '^' => new Coord(p.Key.x, (p.Key.y - t + H) % H),
                        _ => throw new Exception("wtf")
                    }).ToHashSet();

            var seen = new HashSet<(Coord p, int t, bool e1, bool s1)>();
            var queue = new Deque<(Coord p, int t, bool e1, bool s1)>(new[] { (S, 0, false, false) });

            var part2 = false;
            while(queue.Any())
            {
                var state = queue.RemoveFromFront();
                if (OutOfBounds(state.p, W, H))
                    continue;
                if (state.p == E)
                    if (!part2)
                    {
                        Ans(state.t);
                        break;
                    }
                    else if (state.s1 && state.e1)
                    {
                        Ans2(state.t);
                        break;
                    }
                    else state.e1 = true;
                else if (state.p == S && state.e1)
                    state.s1 = true;
                if (seen.Contains(state))
                    continue;
                seen.Add(state);

                foreach (var v in new []{ new GridVector(0, 0), GridVector.E, GridVector.N, GridVector.W, GridVector.S })
                    if (!bliz[state.t + 1].Contains(state.p + v))
                        queue.AddToBack((state.p + v, state.t + 1, state.e1, state.s1));
            }
        }
    }
}

