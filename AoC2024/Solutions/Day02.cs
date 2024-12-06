namespace AoC2024.Solutions
{
    public class Day02() : BaseDay(2024)
    {
        List<List<int>> Reports = [];
        public override void ProcessInput()
        {
            Reports = Input.Extract<List<int>>(@"((\d+)\s?)+").ToList();
        }

        public override dynamic Solve_1()
        {
            var safe = 0;
            foreach (var report in Reports)
            {
                if (increasing(report).Count != report.Count && decreasing(report).Count != report.Count) continue;
                safe++;
            }
            return safe;
        }

        public override dynamic Solve_2()
        {
            var safe = 0;
            foreach (var report in Reports)
            {
                if (increasing(report).Count < (report.Count - 1) && decreasing(report).Count < (report.Count - 1)) continue;
                safe++;
            }
            return safe;
        }

        static List<int> increasing(List<int> report)
        {
            for (int i = 1; i < report.Count; i++)
                if (report[i] <= report[i - 1] || Math.Abs(report[i] - report[i-1]) > 3)
                {
                    var n = report[..i];
                    n.AddRange(report[(i + 1)..]);
                    return increasing(n);
                }
            return report;
        }

        static List<int> decreasing(List<int> report)
        {
            for (int i = 1; i < report.Count; i++)
                if (report[i] >= report[i - 1] || Math.Abs(report[i] - report[i - 1]) > 3)
                {
                    var n = report[..i];
                    n.AddRange(report[(i + 1)..]);
                    return decreasing(n);
                }
            return report;
        }
    }
}
