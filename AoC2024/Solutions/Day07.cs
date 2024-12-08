namespace AoC2024.Solutions
{
    public class Day07() : BaseDay(2024)
    {
        List<(long target, List<long> operands)> Calibration = [];
        public override void ProcessInput()
        {
            Calibration = Input.Extract<(long target, List<long> operands)>(@"(\d+): (\d+ ?)+").ToList();
        }

        static bool IsValid(long target, List<long> operands, bool part2=false)
        {
            if (operands.Count == 1) return operands[0] == target;
            if (target < operands[0]) return false;
            var a = operands[0]; var b = operands[1]; var remaining = operands[2..];
            if (IsValid(target, [a + b, .. remaining], part2)) return true;
            if (IsValid(target, [a * b, .. remaining], part2)) return true;
            if (part2 && IsValid(target, [a * (long)Math.Pow(10, b.CountDigits()) + b, .. remaining], part2)) return true;
            return false;
        }

        public override dynamic Solve_1() 
            => Calibration.Where(p => IsValid(p.target, p.operands)).Sum(p => p.target);

        public override dynamic Solve_2() 
            => Calibration.Where(p => IsValid(p.target, p.operands, true)).Sum(p => p.target);
    }
}
