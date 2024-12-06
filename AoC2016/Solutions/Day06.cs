namespace AoC2016.Solutions
{
    internal class Day06 : BaseDay2016
    {
        public override void ProcessInput()
        {

        }

        public override dynamic Solve_1()
        {
            return string.Join("", Enumerable.Range(0, Input[0].Length)
                .Select(i => Input.Select(l => l[i])
                    .GroupBy(c => c)
                    .Select(g => (g.Key, g.Count()))
                    .OrderByDescending(pair => pair.Item2)
                    .First().Key));
        }

        public override dynamic Solve_2()
        {
            return string.Join("", Enumerable.Range(0, Input[0].Length)
                .Select(i => Input.Select(l => l[i])
                    .GroupBy(c => c)
                    .Select(g => (g.Key, g.Count()))
                    .OrderBy(pair => pair.Item2)
                    .First().Key));
        }
    }
}
