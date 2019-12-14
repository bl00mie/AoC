using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2015._25
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            Regex rx = new Regex(@" (?<row>\d+), column (?<col>\d+).");
            var input = rx.Match(AoCUtil.GetAocInput(2015, 25).First());
            var row = long.Parse(input.Groups["row"].Value);
            var col = long.Parse(input.Groups["col"].Value);

            #endregion

            #region Part 1
            long num = 1;
            for (int i = 2; i <= row; i++)
                num += i - 1;
            for (int i = 1; i < col; i++)
                num += i + row;

            long val = 20151125;
            for (int i=2; i<=num; i++)
                val = val * 252533 % 33554393;

            Ans(val);
            #endregion Part 1
        }
    }
}

