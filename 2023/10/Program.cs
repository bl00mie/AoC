using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC._2023._10
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 10)
                .SelectMany((l, y) => l.Select((c, x) => (new Coord(x,y), c)))
                .ToDictionary(x => x.Item1, x => x.c);
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var S = input.Where(p => p.Value == 'S').Single();

            (Coord a, Coord b) Exits(Coord p) => input[p] switch
            {
                '|' => (p + GridVector.N, p + GridVector.S),
                '-' => (p + GridVector.E, p + GridVector.W),
                'L' => (p + GridVector.E, p + GridVector.S),
                'F' => (p + GridVector.N, p + GridVector.E),
                '7' => (p + GridVector.W, p + GridVector.N),
                'J' => (p + GridVector.S, p + GridVector.W),
                _ => throw new Exception("fuuu")
            };
            var FL = new List<Coord>();
            foreach (var v in GridVector.DirsESNW)
            {
                var (a, b) = Exits(S.Key + v);
                if (a == S.Key || b == S.Key) FL.Add(S.Key + v);
            }

            var path = new HashSet<Coord>() { S.Key, FL[0] };
            while (FL[0] != FL[1])
            {
                var (a, b) = Exits(FL[0]);
                FL[0] = path.Contains(b) ? a : b;
                path.Add(FL[0]);
            }

            Ans(path.Count / 2 + path.Count % 2);
            #endregion Part 1

            #region Part 2
            var crosses = new HashSet<char>() { '-', 'J', '7' };
            foreach (var p in input.Where(kv => !path.Contains(kv.Key)))
                input[p.Key] = '.';
            foreach (var p in input.Where(kv => kv.Value == '.'))
            {
                var trace = p.Key;
                var cross = 0;
                while (trace.y != 0)
                {
                    if (crosses.Contains(input[trace]))
                        cross++;
                    trace += GridVector.S;
                }
                input[p.Key] = cross % 2 == 0 ? 'O' : 'I';
            }

            Ans2(input.Count(p => p.Value == 'I'));
            #endregion
        }
    }
}

