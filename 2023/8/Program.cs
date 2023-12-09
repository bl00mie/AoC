using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using RegExtract;

namespace AoC._2023._8
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GroupInput(2023, 8);
            var dirs = new Directions(input.First().First());
            var maps = input.Last()
                .Extract<(string p, string l, string r)>(@"(\w+) = \((\w+), (\w+)\)")
                .ToDictionary(x => x.p, x => (x.l, x.r));
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var pos = "AAA";
            int ans = 0;
            while (pos != "ZZZ")
            {
                var map = maps[pos];
                pos = dirs.next() == 'L' ? map.l : map.r;
                ans++;
            }
            Ans(ans);
            #endregion Part 1

            #region Part 2
            var positions = maps.Keys.Where(p => p[2] == 'A').ToList();
            dirs.pos = -1;
            var steps = 0;
            var Z = new int[positions.Count];
            while (Z.Any(v => v==0))
            {
                var dir = dirs.next();
                for (int i=0; i<positions.Count; i++)
                {
                    var (l, r) = maps[positions[i]];
                    positions[i] = dir == 'L' ? l : r;
                    if (positions[i][2] == 'Z')
                        if (Z[i] == 0) Z[i] = steps;
                }
                steps++;
            }

            Ans2(steps);
            #endregion
        }
    }

    class Directions
    {
        List<char> dirs;
        public int pos = -1;

        public Directions(IEnumerable<char> input)
        {
            dirs = input.ToList();
        }

        public char next()
        {
            pos = (pos + 1) % dirs.Count;
            return dirs[pos];
        }

        public char prev()
        {
            pos = (pos + dirs.Count - 1) % dirs.Count;
            return dirs[pos];
        }
    }
}

