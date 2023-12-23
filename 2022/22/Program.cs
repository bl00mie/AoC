using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AoCUtil.Collections;

namespace AoC._2022._22
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region constants
            const int E = 0;
            const int N = 1;
            const int W = 2;
            const int S = 3;
            var dirs = new Dictionary<int, GridVector>
            {
                [E] = GridVector.E,
                [N] = GridVector.N,
                [W] = GridVector.W,
                [S] = GridVector.S
            }.ToImmutableDictionary();
            Coord START = new(0,0);
            //foreach (var (p, v) in grid)
            //    if (v == ' ') continue;
            //    else
            //    {
            //        START = p;
            //        break;
            //    }

            var len = START.x;

            var AOX = len;
            var AOY = 0;
            var BOX = len * 2;
            var BOY = 0;
            var COX = len;
            var COY = len;
            var DOX = 0;
            var DOY = len * 2;
            var EOX = len;
            var EOY = len * 2;
            var FOX = 0;
            var FOY = len * 3;
            #endregion

            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GroupInput(2022, 22).ToArray();
            var grid = new Grid<char>(input[0].SelectMany((l, y) => l.Select((c, x) => (x, y, c))))
            {
                StoreOnMissingLookup = true,
                DefaultValue = ' '
            };
            var dist = input[1].First().Split("RL".ToCharArray()).Select(int.Parse).ToArray();
            var turn = input[1].First().Where(c => c == 'R' || c == 'L').ToArray();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            //. A B;
            //. C
            //D E
            //F
            char region(Coord p)
                => (p.x / len, p.y / 50) switch
                {
                    (1, 0) => 'A',
                    (2, 0) => 'B',
                    (1, 1) => 'C',
                    (0, 2) => 'D',
                    (1, 2) => 'E',
                    (0, 3) => 'F',
                    _ => throw new Exception("wtf")
                };

            var next = new[] {
                // part 1
                new Dictionary<(char r, int d), Func<Coord, (Coord np, int d)>>
                {
                    [('A', W)] = (p) => (new(BOX + len - 1, p.y), W),
                    [('A', S)] = (p) => (new(p.x, EOY + len - 1), S),

                    [('B', E)] = (p) => (new(AOX, p.y), E),
                    [('B', N)] = (p) => (new (p.x, BOY), N),
                    [('B', S)] = (p) => (new(p.x, BOY + len - 1), S),

                    [('C', E)] = (p) => (new(COX, p.y), E),
                    [('C', W)] = (p) => (new(COX + len - 1, p.y), W),

                    [('D', W)] = (p) => (new(EOX + len - 1, p.y), W),
                    [('D', S)] = (p) => (new(p.x, FOY + len - 1), S),

                    [('E', E)] = (p) => (new(DOX, p.y), E),
                    [('E', N)] = (p) => (new(p.x, AOY), N),

                    [('F', E)] = (p) => (new(FOX, p.y), E),
                    [('F', N)] = (p) => (new(p.x, DOY), N),
                    [('F', W)] = (p) => (new(FOX + len - 1, p.y), W),
                },
                // part 2
                new Dictionary<(char r, int d), Func<Coord, (Coord np, int d)>>
                {
                    [('A', W)] = (p) => (new(DOX, DOY + len - p.y % len), E),
                    [('A', S)] = (p) => (new(FOX, FOY + p.x % len), E),

                    [('B', E)] = (p) => (new(EOX + len - 1, EOY + len - p.y % len), W),
                    [('B', N)] = (p) => (new(COX + len - 1, COY + p.y), W),
                    [('B', S)] = (p) => (new(FOX + p.x % len, FOY + len - 1), S),

                    [('C', E)] = (p) => (new(BOX + p.y % len, BOY + len - 1), S),
                    [('C', W)] = (p) => (new(DOX + p.y % len, DOY), N),

                    [('D', W)] = (p) => (new(AOX, AOY + len - p.y % len), E),
                    [('D', S)] = (p) => (new(COX, COY + p.y % len), E),

                    [('E', E)] = (p) => (new(BOX + len - 1, BOY + len - p.y % len), W),
                    [('E', N)] = (p) => (new(FOX + len - 1, FOY + p.x % len), W),

                    [('F', E)] = (p) => (new(EOX + p.y % len, EOX + len - 1), S),
                    [('F', N)] = (p) => (new(BOX + p.x % len, BOY), N),
                    [('F', W)] = (p) => (new(AOX + p.y % len, AOY), N),
                }
            };

            (Coord, int) Walk(Coord p, int d, int distance, int part = 0)
            {
                while (distance-- > 0)
                {
                    switch(grid[p + dirs[d]])
                    {
                        case '#': return (p, d);
                        case '.': p += dirs[d]; break;
                        case ' ':
                            var r = region(p);
                            var (np, nd) = next[part][(r, d)](p);
                            if (grid[np] == '.')
                            {
                                p = np;
                                d = nd;
                            }
                            break;
                    }
                }
                return (p, d);
            }

            foreach (var part in Enumerable.Range(0,2))
            {
                var d = 0;
                var p = new Coord(START);
                var i = 0;
                (p, d) = Walk(p, d, dist[0], part);
                do
                {
                    if (turn[i] == 'R') d = (d + 1) % 4;
                    else d = (d + 3) % 4;
                    (p,d) = Walk(p, d, dist[i + 1], part);
                }
                while (++i < turn.Length);

                Ans((p.y + 1) * 1000 + (p.x + 1) * 4 + d, part+1);
            }
        }
    }
}

