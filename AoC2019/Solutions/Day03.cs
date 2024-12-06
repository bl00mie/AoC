using AoCUtil.Collections;
using AoCUtil;

namespace AoC2019.Solutions
{
    public class Day03() : BaseDay(2019)
    {
        readonly Dictionary<char, GridVector> Dirs = new()
        {
            ['U'] = GridVector.S,
            ['D'] = GridVector.N,
            ['R'] = GridVector.E,
            ['L'] = GridVector.W
        };
        (GridVector dir, int magnitude)[][] Wires = [];
        public override void ProcessInput()
        {
            Wires = Input.Extract<List<(char d, int m)>>(@"(([UDLR])(\d+),?)+")
                .Select(wire => wire.Select(segment => (Dirs[segment.d], segment.m)).ToArray())
                .ToArray();
        }

        public override dynamic Solve_1()
        {
            HashSet<Coord> grid = [];
            var best = int.MaxValue;
            var origin = new Coord(0, 0);
            foreach (var wire in Wires)
            {
                var p = origin;
                foreach (var (dir, magnitude) in wire)
                    foreach (var _ in Enumerable.Range(0, magnitude))
                    {
                        p += dir;
                        if (grid.Contains(p))
                            best = Math.Min(best, AoC.AoCUtil.ManhattanDistance(origin, p));
                        grid.Add(p);
                    }
            }
            return best;
        }

        public override dynamic Solve_2()
        {
            Dictionary<Coord, int>[] wirePoints = [[], []];
            var origin = new Coord(0, 0);
            foreach (var wi in Enumerable.Range(0,2))
            {
                var wire = Wires[wi];
                var point = origin;
                var step = 0;
                foreach (var (dir, magnitude) in wire)
                {
                    foreach (var _ in Enumerable.Range(0, magnitude))
                    {
                        step++;
                        point += dir;
                        if (!wirePoints[wi].ContainsKey(point))
                            wirePoints[wi][point] = step;
                    }
                }
            }
            return wirePoints[0]
                .Where(pair => wirePoints[1].ContainsKey(pair.Key))
                .Select(pair => pair.Value + wirePoints[1][pair.Key])
                .Min();
        }
    }
}
