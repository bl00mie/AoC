namespace AoC2023.Solutions
{
    internal class Day18 : BaseDay2023
    {
        (char dir, int count, string hex)[] digPlan = [];
        public override void ProcessInput()
        {
            digPlan = Input.Extract<(char dir, int count, string hex)>(@"([UDLR]) (\d+) \(#([0-9a-f]+)\)").ToArray();
        }

        public override dynamic Solve_1()
        {
            Dictionary<char, (int dx, int dy)> dirs = new()
            {
                ['U'] = (0, -1),
                ['R'] = (1, 0),
                ['D'] = (0, 1),
                ['L'] = (-1, 0)
            };
            return CountDigs(digPlan.Select(dig => (dirs[dig.dir], dig.count)));
        }

        public override dynamic Solve_2()
        {
            Dictionary<char, (int dx, int dy)> dirs = new()
            {
                ['0'] = (1, 0),
                ['1'] = (0, 1),
                ['2'] = (-1, 0),
                ['3'] = (0, -1)
            };

            return CountDigs(digPlan.Select(x => (dirs[x.hex[^1]], Convert.ToInt32(x.hex[..^1], 16))));
        }

        static double CountDigs(IEnumerable<((int dx, int dy) dir, int count)> digs)
        {
            // dig points around the circumference
            var border = 0L;
            List<(int x, int y)> points = [(0, 0)];
            foreach (var (dir, n) in digs)
            {
                border += n;
                var (x, y) = points[^1];
                points.Add((x + dir.dx * n, y + dir.dy * n));
            }

            var len = points.Count;
            // Shoelace formula area
            var A = points.Zip(points.Skip(1).Append(points[0]))
                .Sum(pair => ((long)pair.First.y + pair.Second.y) * (pair.First.x - pair.Second.x)) / 2;
            // pick's theorem
            return A + 1 + border/2;
        }

    }
}
