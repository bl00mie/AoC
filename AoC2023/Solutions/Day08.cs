using System.Collections.Immutable;

namespace AoC2023.Solutions
{
    internal class Day08 : BaseDay2023
    {
        Directions dirs = new(Enumerable.Empty<char>());
        Dictionary<string, (string l, string r)> maps = [];

        public override void ProcessInput()
        {
            var groups = AoC.AoCUtil.GroupInput(Input);
            dirs = new Directions(groups.First().First());
            maps = groups.Last()
                .Extract<(string p, string l, string r)>(@"(\w+) = \((\w+), (\w+)\)")
                .ToDictionary(x => x.p, x => (x.l, x.r));
        }

        public override dynamic Solve_1()
        {
            var pos = "AAA";
            int ans = 0;
            while (pos != "ZZZ")
            {
                var (l, r) = maps[pos];
                pos = dirs.Next() == 'L' ? l : r;
                ans++;
            }
            return ans;
        }

        public override dynamic Solve_2()
        {
            var positions = maps.Keys.Where(p => p[2] == 'A').ToList();
            dirs.Reset();
            ulong steps = 0L;
            var Z = new ulong[positions.Count];
            var cycles = new ulong[positions.Count];
            while (cycles.Any(v => v == 0))
            {
                var dir = dirs.Next();
                for (int i = 0; i < positions.Count; i++)
                {
                    var (l, r) = maps[positions[i]];
                    positions[i] = dir == 'L' ? l : r;
                    if (positions[i][2] == 'Z')
                        if (Z[i] == 0) Z[i] = steps;
                        else if (cycles[i] == 0) cycles[i] = steps - Z[i];
                }
                steps++;
            }

            return cycles.Aggregate(AoC.AoCUtil.LCM);
        }

        class Directions(IEnumerable<char> input)
        {
            private readonly ImmutableList<char> dirs = input.ToImmutableList();
            private int Pos = -1;

            public char Next()
            {
                Pos = (Pos + 1) % dirs.Count;
                return dirs[Pos];
            }

            public char Prev()
            {
                Pos = (Pos + dirs.Count - 1) % dirs.Count;
                return dirs[Pos];
            }

            public void Reset()
            {
                Pos = -1;
            }
        }
    }
}
