using RegExtract;

namespace AoC2016.Solutions
{
    internal class Day03 : BaseDay2016
    {
        List<int>[] nums = [];
        public override void ProcessInput()
        {
            nums = Input.Extract<List<int>>(@"((\d+) *)+")
                .ToArray();
        }

        public override dynamic Solve_1()
        {
            var sorted = nums.Select(x => { x.Sort(); return x; }).ToArray();
            return sorted.Count(n => n[0] + n[1] > n[2]);
        }

        public override dynamic Solve_2()
        {
            var ans = 0;
            foreach (var chunk in nums.Chunk(3))
            {
                foreach (var i in Enumerable.Range(0, 3))
                {
                    var t = new[] { chunk[0][i], chunk[1][i], chunk[2][i] };
                    Array.Sort(t);
                    if (t[0] + t[1] > t[2]) ans++;
                }
            }

            return ans;
        }
    }
}
