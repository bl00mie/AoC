namespace AoC2023.Solutions
{
    internal class Day01 : BaseDay2023
    {
        List<string> input = [];

        public override void ProcessInput()
        {
            input = [.. Input];
        }

        static readonly string[] words = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        static int CalibrationValues(string s, bool p2 = false)
        {
            List<int> nums = [];
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                    nums.Add(s[i] - '0');
                else if (p2)
                    foreach (var (w, j) in words.Select((w, i) => (w, i)))
                        if (s[i..].StartsWith(w))
                        {
                            nums.Add(j + 1);
                            i += w.Length - 2;
                            break;
                        }
            }
            return nums[0] * 10 + nums[^1];
        }

        public override string Solve_1()
            => input.Sum(l => CalibrationValues(l)).ToString();

        public override string Solve_2()
            => input.Sum(l => CalibrationValues(l, true)).ToString();
    }
}
