using System.Collections.Generic;
using System.Linq;

namespace AoC._2022._17
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var jets = AoCUtil.GetAocInput(2022, 17).First();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            const long MAX_ROCKS = 1_000_000_000_000;

            HashSet<(long x, long y)> tower = new[] { (0L, 0L), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0) }.ToHashSet();
            var rocks = 0L;
            var highest = 0L;
            var i = 0;
            var states = new Dictionary<(int, long, string), (long rocks, long highest)>();
            var added = 0L;
            while(rocks < MAX_ROCKS)
            {
                var piece = Piece((int)(rocks % 5), highest+4);
                while(true)
                {
                    HashSet<(long x, long y)> next;
                    if (jets[i] == '>')
                        next = Right(piece);
                    else
                        next = Left(piece);
                    if (!next.Intersect(tower).Any())
                        piece = next.ToHashSet();
                    i = (i + 1) % jets.Length;
                    
                    next = Down(piece);
                    if (!next.Intersect(tower).Any())
                        piece = next.ToHashSet();
                    else
                    {
                        foreach (var c in piece)
                            tower.Add(c);
                        highest = tower.Max(c => c.y);
                        
                        var last20 = string.Join(",", tower
                            .Where(c => highest - c.y <= 20)
                            .Select(c => $"({c.x},{highest - c.y})")
                            .OrderBy(s => s));
                        var state = (i, rocks % 5, last20);
                        if (states.ContainsKey(state))
                        {
                            var prev = states[state];
                            var dy = highest - prev.highest;
                            var drocks = rocks - prev.rocks;
                            var a = (MAX_ROCKS - rocks) / drocks;
                            added += a * dy;
                            rocks += a * drocks;
                        }
                        states[state] = (rocks, highest);

                        break;
                    }
                }
                rocks++;
                if (rocks == 2022)
                    Ans(highest);
            }
            Ans2(highest + added);
        }

        static HashSet<(long x,long y)> Piece(long type, long y)
            => type switch
                {
                    0 => new[] { (2L, y), (3, y), (4, y), (5, y) }.ToHashSet(),
                    1 => new[] { (3L, y + 2), (2, y + 1), (3, y + 1), (4, y + 1), (3, y) }.ToHashSet(),
                    2 => new[] { (2L, y), (3, y), (4, y), (4, y + 1), (4, y + 2) }.ToHashSet(),
                    3 => new[] { (2L, y), (2, y + 1), (2, y + 2), (2, y + 3) }.ToHashSet(),
                    4 => new[] { (2L, y + 1), (2, y), (3, y + 1), (3, y) }.ToHashSet(),
                    _ => null,
                };

        static HashSet<(long x,long y)> Right(HashSet<(long x, long y)> p)
        {
            if (p.Any(c => c.x == 6)) return p.ToHashSet();
            return p.Select(c => (c.x + GridVector.E.dx, c.y + GridVector.E.dy)).ToHashSet();
        }

        static HashSet<(long x, long y)> Left(HashSet<(long x, long y)> p)
        {
            if (p.Any(c => c.x == 0)) return p.ToHashSet();
            return p.Select(c => (c.x + GridVector.W.dx, c.y + GridVector.W.dy)).ToHashSet();
        }

        static HashSet<(long x, long y)> Down(HashSet<(long x, long y)> p)
            => p.Select(c => (c.x + GridVector.S.dx, c.y + GridVector.S.dy)).ToHashSet();
    }
}

