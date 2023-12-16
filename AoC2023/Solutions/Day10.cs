namespace AoC2023.Solutions
{
    internal class Day10 : BaseDay2023
    {
        Dictionary<Coord, char> grid = [];
        HashSet<Coord> path = [];

        public override void ProcessInput()
        {
            grid = Input.SelectMany((line, y) => line.Select((c, x) => (new Coord(x, y), c)))
                .ToDictionary(location => location.Item1, location => location.c);
        }

        public override dynamic Solve_1()
        {
            var S = grid.Where(p => p.Value == 'S').Single();

            (Coord a, Coord b) Exits(Coord p) => grid[p] switch
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

            path = [S.Key, FL[0]];
            while (FL[0] != FL[1])
            {
                var (a, b) = Exits(FL[0]);
                FL[0] = path.Contains(b) ? a : b;
                path.Add(FL[0]);
            }

            return path.Count / 2 + path.Count % 2;
        }

        public override dynamic Solve_2()
        {
            var crosses = new HashSet<char>() { '-', 'J', '7' };
            foreach (var p in grid.Where(kv => !path.Contains(kv.Key)))
                grid[p.Key] = '.';
            foreach (var p in grid.Where(kv => kv.Value == '.'))
            {
                var trace = p.Key;
                var cross = 0;
                while (trace.y != 0)
                {
                    if (crosses.Contains(grid[trace]))
                        cross++;
                    trace += GridVector.S;
                }
                grid[p.Key] = cross % 2 == 0 ? 'O' : 'I';
            }

            return grid.Count(p => p.Value == 'I');
        }
    }
}
