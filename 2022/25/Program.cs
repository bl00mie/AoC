using System.Linq;
using Nito.Collections;

namespace AoC._2022._25
{
    class Program : ProgramBase
    {
        static void Main()
        {
            var input = AoCUtil.GetAocInput(2022, 25).ToArray();
            Stopwatch.Restart();

            var sum = input.Sum(line => line.Aggregate(0L, (acc, ch) => acc * 5 + ch switch { '-' => -1, '=' => -2, _ => (int)(ch - '0') }));
            var snits = new Deque<int>();
            var c = 0;
            while (sum > 0 || c > 0)
                snits.AddToFront(Snit(ref sum, ref c));
            Ans(string.Join("", snits.Select(s => s switch { -2 => '=', -1 => '-', _ => (char)(s + '0')})));
            Ans2("Merry Christmas!");
        }

        static int Snit(ref long sum, ref int c)
        {
            var d = (int)(sum % 5 + c);
            (d, c) = d > 2 ? (d - 5, 1) : (d, 0);
            sum /= 5;
            return d;
        }
    }
}

