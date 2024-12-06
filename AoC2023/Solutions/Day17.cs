using AoCUtil.Collections;

namespace AoC2023.Solutions
{
    internal class Day17() : BaseDay(2023)
    {
        Grid<int> grid = new();
        public override void ProcessInput()
        {
            grid = new Grid<int>(Input.Select(l => l.Select(c => c - '0')));
        }

        int Solve(bool part2 = false)
        {
            var Q = new PriorityQueue<(int x, int y, int dir, int same), int>();
            Q.Enqueue((0, 0, -1, -1), 0);
            var heatLost = new Dictionary<(int x, int y, int dir, int same), int>();
            var dirs = new[] { (0, -1), (1, 0), (0, 1), (-1, 0) };
            while(Q.Count > 0)
            {
                Q.TryDequeue(out var v, out int heat);
                if (heatLost.ContainsKey(v))
                    continue;
                heatLost[v] = heat;
                foreach (var (dx, dy, i) in dirs.Select((p, i) => (p.Item1, p.Item2, i)))
                {
                    var x = v.x + dx;
                    var y = v.y + dy;
                    var dir = i;
                    var same = (dir != v.dir ? 1 : v.same + 1);

                    var isValid = (part2 && same <= 10 && (dir == v.dir || same >= 4 || same == -1)) || (!part2 && same < 3);

                    if (grid.ContainsCoord(new(x, y)) && 
                        ((dir + 2) % 4 != v.dir) &&
                        isValid)
                    {
                        Q.Enqueue((x, y, dir, same), heat + grid[x, y]);
                    }
                }
            }
            return heatLost.Where(pair => pair.Key.x == grid.W - 1 && pair.Key.y == grid.H - 1).Min(pair => pair.Value);

        }

        public override dynamic Solve_1()
        {
            return Solve();
        }

        public override dynamic Solve_2()
        {
            return Solve(true);
        }
    }
}
