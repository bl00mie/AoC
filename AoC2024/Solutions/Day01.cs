namespace AoC2024.Solutions
{
    public class Day01() : BaseDay(2024)
    {
        readonly List<int> left = [];
        readonly List<int> right = [];
        public override void ProcessInput()
        {
            left.Clear();
            right.Clear();
            foreach (var (l, r) in Input.Extract<(int, int)>(@"(\d+)\s+(\d+)"))
            {
                left.Add(l);
                right.Add(r);
            }
        }

        public override dynamic Solve_1()
        {
            left.Sort();
            right.Sort();
            return Enumerable.Range(0, left.Count).Sum(i => Math.Abs(left[i] - right[i]));
        }

        public override dynamic Solve_2() => 
            left.Sum(l => l * right.Where(l.Equals).Count());
    }
}
