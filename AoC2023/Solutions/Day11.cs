namespace AoC2023.Solutions
{
    internal class Day11() : BaseDay(2023)
    {
        long Expand(int delta)
        {
            var ER = Enumerable.Range(0, Input.Length)
                    .Where(i => Input[i].All(c => c == '.'))
                    .ToHashSet();
            var EC = Enumerable.Range(0, Input[0].Length)
                .Where(i => Input.All(l => l[i] == '.'))
                .ToHashSet();
            var G = Input.SelectMany((l, y) =>
                l.Select((c, x) => (x, y, c))
                .Where(t => t.c == '#')
                .Select(t => (t.x, t.y)))
                .ToList();

            return G.ForAllPairs<(int x, int y), long>((a, b) =>
            {
                var d = 0;
                for (int x = a.x; x != b.x; x += a.x < b.x ? 1 : -1)
                    d += 1 + (EC.Contains(x) ? delta : 0);
                for (int y = a.y; y != b.y; y += a.y < b.y ? 1 : -1)
                    d += 1 + (ER.Contains(y) ? delta : 0);
                return d;
            }).ToArray().Sum();
        }


        public override dynamic Solve_1() => Expand(1);

        public override dynamic Solve_2() => Expand(999_999);
    }
}
