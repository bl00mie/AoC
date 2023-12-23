using AoCUtil.Collections;
using Nito.Collections;
using System.Collections.Immutable;

namespace AoC2023.Solutions.Solutions
{
    internal class Day23() : BaseDay(2023)
    {
        readonly Dictionary<char, GridVector> Dirs = new Dictionary<char, GridVector>
        {
            ['>'] = GridVector.E,
            ['v'] = GridVector.N,
            ['<'] = GridVector.W,
            ['^'] = GridVector.S
        };

        Grid<char> grid = new();
        Coord Start = new(0,0);
        HashSet<Coord> Forks = [];

        public override void ProcessInput()
        {
            grid = new Grid<char>(Input);
            Start = new (Input[0].IndexOf('.'), 0);
            Forks = grid.Where(pv => pv.v != '#' && 
                    GridVector.DirsNESW.Count(gv => grid.ContainsCoord(pv.p + gv) && grid[pv.p + gv] != '#') > 2)
                .Select(pv => pv.p)
                .ToHashSet();
            Forks.Add(Start);
            Forks.Add(new(Input[^1].IndexOf('.'), grid.H - 1));
        }


        int  Hike(bool part1 = true)
        {
            var edges = new Dictionary<Coord, List<(Coord exit, int steps)>>();
            foreach (Coord sp in Forks)
            {
                edges[sp] = [];
                var Q = new Deque<(Coord p, int steps)>();
                Q.AddToBack((sp, 0));
                var seen = new HashSet<Coord>();
                while (Q.Count > 0)
                {
                    var (ep, steps) = Q.RemoveFromFront();
                    if (seen.Contains(ep)) continue;
                    seen.Add(ep);
                    if (Forks.Contains(ep) && ep != sp)
                    {
                        edges[sp].Add((ep, steps));
                        continue;
                    }
                    foreach (var dir in Dirs)
                    {
                        var n = ep + dir.Value;
                        if (grid.ContainsCoord(n) && grid[n] != '#')
                        {
                            if (part1 && Dirs.ContainsKey(grid[ep]) && grid[ep] != dir.Key) continue;
                            Q.AddToBack((n, steps + 1));
                        }
                    }
                }
            }

            var ans = 0;
            void DFS(Coord p, int steps, ImmutableHashSet<Coord> seen)
            {
                if (p.y == grid.H - 1)
                    ans = Math.Max(ans, steps);
                foreach (var thing in edges[p])
                {
                    if (seen.Contains(thing.exit)) continue;
                    DFS(thing.exit, steps + thing.steps, seen.Add(thing.exit));
                }
            }
            
            DFS(Start, 0, []);
            return ans;

        }

        public override dynamic Solve_1() => Hike();

        public override dynamic Solve_2() => Hike(false);
    }
}
