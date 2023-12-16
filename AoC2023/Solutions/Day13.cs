namespace AoC2023.Solutions
{
    internal class Day13 : BaseDay2023
    {
        List<List<string>> groups = [];

        public override void ProcessInput()
        {
            groups = AoC.AoCUtil.GroupInput(Input).Select(x => x.ToList()).ToList();
        }

        private static int ScoreMirror(List<string> grid, int part = 1)
        {
            var rows = grid.Count;
            var cols = grid[0].Length;
            int bad;
            foreach (var col in Enumerable.Range(0, cols - 1))
            {
                bad = 0;
                foreach (var dc in Enumerable.Range(0, cols))
                {
                    var left = col - dc;
                    var right = col + 1 + dc;
                    if (0 <= left && left < right && right < cols)
                        foreach (var row in Enumerable.Range(0, rows))
                            if (grid[row][left] != grid[row][right])
                                bad++;
                }
                if (bad == part - 1)
                    return col + 1;
            }
            foreach (var row in Enumerable.Range(0, rows - 1))
            {
                bad = 0;
                foreach (var dr in Enumerable.Range(0, rows))
                {
                    var top = row - dr;
                    var bot = row + 1 + dr;
                    if (0 <= top && top < bot && bot < rows)
                        foreach (var col in Enumerable.Range(0, cols))
                            if (grid[top][col] != grid[bot][col])
                                bad++;
                }
                if (bad == part - 1)
                    return 100 * (row + 1);
            }
            throw new Exception("fuuuuu");
        }

        public override dynamic Solve_1() => groups.Sum(g => ScoreMirror(g));

        public override dynamic Solve_2() => groups.Sum(g => ScoreMirror(g, 2));
    }
}
