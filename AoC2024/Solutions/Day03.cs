using System.Text.RegularExpressions;

namespace AoC2024.Solutions
{
    public class Day03() : BaseDay(2024)
    {
        string Code = "";
        public override void ProcessInput()
        {
            Code = string.Join("", Input);
        }

        public override dynamic Solve_1()
        {
            var rx = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
            return rx.Matches(Code).Sum(match => long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[2].Value));
        }

        public override dynamic Solve_2()
        {
            var rx = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)");
            var enabled = true;
            var total = 0L;
            foreach (Match match in rx.Matches(Code))
            {
                if (match.Value.StartsWith("do()")) enabled = true;
                else if (match.Value.StartsWith("don't()")) enabled = false;
                else if (enabled)
                    total += (long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[2].Value));
            }
            return total;
        }
    }
}
