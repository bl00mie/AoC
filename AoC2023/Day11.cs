namespace AoC2023
{
    internal class Day11 : BaseDay2023
    {
        string[] input = [];

        public override void ProcessInput()
        {
            input = [.. Input];
        }

        static long Expand(string[] input, int delta = 1)
        {
            var ER = Enumerable.Range(0, input.Length)
                .Where(i => input[i].All(c => c == '.'))
                .ToHashSet();
            var EC = Enumerable.Range(0, input[0].Length)
                .Where(i => input.All(l => l[i] == '.'))
                .ToHashSet();
            var G = input.SelectMany((l, y) =>
                l.Select((c, x) => (x, y, c))
                .Where(t => t.c == '#')
                .Select(t => (t.x, t.y)))
                .ToList();

            var ans = G.ForAllPairs<(int x, int y), long>((a, b) =>
            {
                var d = 0;
                for (int x = a.x; x != b.x; x += a.x < b.x ? 1 : -1)
                    d += 1 + (EC.Contains(x) ? delta : 0);
                for (int y = a.y; y != b.y; y += a.y < b.y ? 1 : -1)
                    d += 1 + (ER.Contains(y) ? delta : 0);
                return d;
            }).ToArray().Sum();
            return ans;
        }

        public override string Solve_1() => Expand(input).ToString();

        public override string Solve_2() => Expand(input, 999_999).ToString();
    }
}
