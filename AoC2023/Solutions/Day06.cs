namespace AoC2023.Solutions
{
    internal class Day06() : BaseDay(2023)
    {
        List<string[]> input = [];
        readonly List<(long t, long d)> races = [];
        public override void ProcessInput()
        {
            input = Input.Select(l => l.Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToList();
            for (int i = 0; i < input[0].Length; i++)
                races.Add((long.Parse(input[0][i]), long.Parse(input[1][i])));
        }

        static long Winners(long t, long d)
        {
            long count = 0;
            for (int i = 1; i < t; i++)
                if (i * (t - i) > d) count++;
            return count;
        }

        public override dynamic Solve_1()
        {
            return races.Select(r => Winners(r.t, r.d)).Aggregate(1L, (a, b) => a * b);
        }

        public override dynamic Solve_2()
        {
            var t = long.Parse(string.Join("", input[0]));
            var d = long.Parse(string.Join("", input[1]));
            return Winners(t, d);
        }
    }
}
