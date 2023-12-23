using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;
using AoCUtil;

namespace AoC._2021._19
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var scanners = AoCUtil.GroupInput(2021, 19)
                .Select(g =>
                    g.Skip(1)
                    .Extract<(int x, int y, int z)>(@"(-?\w+),(-?\w+),(-?\w+)")
                ).ToList();

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var known = new List<int>(new []{ 0 });
            var unknown = Enumerable.Range(1, scanners.Count - 1).ToList();
            var positions = new (int x, int y, int z)[scanners.Count];
            positions[0] = (0, 0, 0);
            var knownBeacons = scanners[0].ToHashSet();
            while(unknown.Any())
            {
                int found = -1;
                foreach (var u in unknown)
                {
                    if (found >= 0) break;
                    for (int o = 0; o < 48 && found < 0; o++)
                    {
                        var adjustedUnknown = scanners[u].Select(ub => Orientate(ub, o));
                        var matches = new DefaultDictionary<(int x, int y, int z), int>();
                        foreach (var ub in adjustedUnknown)
                            foreach (var kb in knownBeacons)
                                matches[(kb.x - ub.x, kb.y - ub.y, kb.z - ub.z)]++;
                        foreach (var (p, count) in matches)
                            if (count >= 12)
                            {
                                WL($"found {u} ({p.x},{p.y},{p.z})");
                                positions[u] = p;
                                foreach (var au in adjustedUnknown)
                                    knownBeacons.Add((au.x + p.x, au.y + p.y, au.z + p.z));
                                found = u;
                                break;
                            }
                    }
                }
                if (found >= 0)
                {
                    known.Add(found);
                    unknown.Remove(found);
                }
            }
            Ans(knownBeacons.Count());
            #endregion Part 1

            #region Part 2

            var largest = int.MinValue;
            foreach (var (x, y, z) in positions)
                foreach (var (xx, yy, zz) in positions)
                    largest = Math.Max(largest, Math.Abs(x - xx) + Math.Abs(y - yy) + Math.Abs(z - zz));
            Ans2(largest);
            #endregion
        }

        static void dump(IEnumerable<int> items)
        {
            WL($"[{string.Join(',', items)}]");
        }

        static int[][] perms = new[] { 0, 1, 2 }.GetPermutations().Select(ie => ie.ToArray()).ToArray();
        static Dictionary<((int x, int y, int z) pos, int o), (int x, int y, int  z)> orientated = new ();
        static (int x, int y, int z) Orientate((int x, int y, int z) pos, int o)
        {
            var key = (pos, o);
            if (!orientated.ContainsKey(key))
            {
                int[] facing = new[] { pos.x, pos.y, pos.z };
                if (o % 2 == 0) facing[0] *= -1;
                if (o / 2 % 2 == 0) facing[1] *= -1;
                if (o / 4 % 2 == 0) facing[2] *= -1;
                var perm = perms[o / 8];
                orientated[key] = (facing[perm[0]], facing[perm[1]], facing[perm[2]]);
            }
            return orientated[key];
        }
    }
}

