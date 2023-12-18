namespace AoC2023.Solutions
{
    internal class Day18 : BaseDay2023
    {
        (char dir, int count, string hex)[] digPlan = [];
        Dictionary<char, GridVector> dirs = new Dictionary<char, GridVector>
        {
            ['U'] = GridVector.S,
            ['R'] = GridVector.E,
            ['D'] = GridVector.N,
            ['L'] = GridVector.W
        };
        public override void ProcessInput()
        {
            digPlan = Input.Extract<(char dir, int count, string hex)>(@"([UDLR]) (\d+) \(#([0-9a-f]+)\)").ToArray();
        }

        public override dynamic Solve_1()
        {
            Dictionary<char, GridVector> dirs = new Dictionary<char, GridVector>
            {
                ['U'] = GridVector.S,
                ['R'] = GridVector.E,
                ['D'] = GridVector.N,
                ['L'] = GridVector.W
            };
            return CountDigs(digPlan.Select(dig => (dig.dir, dig.count)), dirs);
        }

        public override dynamic Solve_2()
        {
            Dictionary<char, GridVector> dirs = new Dictionary<char, GridVector>
            {
                ['0'] = GridVector.E,
                ['1'] = GridVector.N,
                ['2'] = GridVector.W,
                ['3'] = GridVector.S
            };

            return CountDigs(digPlan.Select(x => (x.hex[^1], Convert.ToInt32(x.hex[..^1], 16))), dirs);
        }

        double CountDigs(IEnumerable<(char dirChar, int count)> digs, Dictionary<char, GridVector> dirs)
        {
            // dig points around the circumference
            var b = 0L;
            List<(int x, int y)> points = [(0, 0)];
            foreach (var (dc, n) in digs)
            {
                b += n;
                var (x, y) = points[^1];
                var dir = dirs[dc];
                points.Add((x + dir.dx * n, y + dir.dy * n));
            }

            var len = points.Count;
            // Shoelace formula area
            var a = 0L;
            for (int i = 0; i < len; i++)
                a += (((long)points[i].y) * (((long)points[i == 0 ? len - 1 : i].x) - ((long)points[i == (len - 1) ? 0 : i + 1].x)));
            // pick's theorem
            return a - b / 2 + 1 + b;
        }

    }
}
