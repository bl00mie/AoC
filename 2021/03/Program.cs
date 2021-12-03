using System.Collections.Generic;
using System.Linq;

namespace AoC._2021._03
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2021, 03).ToArray();
            #endregion

            #region Part 1
            var gamma = 0;
            var epsilon = 0;
            var mul = 1;
            for (int x = input[0].Length - 1; x >= 0; x--)
            {
                if (Ones(input, x).Count() > input.Length / 2.0)
                    gamma += mul;
                else
                    epsilon += mul;
                mul *= 2;
            }

            Ans(gamma * epsilon);

            #endregion Part 1

            #region Part 2
            IEnumerable<string> o2 = input.ToArray(), co2 = input.ToArray();
            for (int x = 0; x < input[0].Length; x++)
            {
                if (o2.Count() > 1)
                {
                    var ones = Ones(o2, x);
                    o2 = ones.Count() >= o2.Count() / 2.0 ? ones : o2.Except(ones);
                }

                if (co2.Count() > 1)
                {
                    var ones = Ones(co2, x);
                    co2 = ones.Count() < co2.Count() / 2.0 ? ones : co2.Except(ones);
                }
            }

            Ans(o2.First().Int(2) * co2.First().Int(2), 2);
            #endregion
        }

        static IEnumerable<string> Ones(IEnumerable<string> strs, int x)
        {
            return strs.Where(d => d[x] == '1');
        }
    }
}

