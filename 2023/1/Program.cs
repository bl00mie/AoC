using System.Collections.Generic;
using System.Linq;

namespace AoC._2023._1
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 1).ToList();

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            Ans(input.Sum(l => CalibrationValues(l)));
            Ans2(input.Sum(l => CalibrationValues(l, true)));
        }

        static readonly string[] words = new[] { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        static int CalibrationValues(string s, bool p2 = false)
        {
            List<int> nums = new();
            for(int i=0; i<s.Length;i++)
            {
                if (char.IsDigit(s[i]))
                    nums.Add(s[i]-'0');
                else if (p2)
                    for (int j=1; j<=9; j++)
                        if (s[i..].StartsWith(words[j]))
                        {
                            nums.Add(j);
                            i += words[j].Length - 2;
                            break;
                        }
            }
            return nums[0]*10 + nums[^1];
        }
    }
}

