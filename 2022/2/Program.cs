using System.Linq;
using RegExtract;

namespace AoC._2022._2
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2022, 2).Extract<(char l, char r)>(@"(\w) (\w)").ToList();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var ans1 = 0;
            foreach (var (l,r) in input)
            {
                var a = l - 'A' + 1;
                var b = r - 'W';
                ans1 += ((b-a+3)%3) switch
                {
                    1 => 6,
                    0 => 3,
                    _ => 0
                } + b;
            }
            Ans(ans1);
            #endregion Part 1

            #region Part 2
            var ans2 = 0;
            foreach (var (l, r) in input)
            {
                var a = l - 'A';
                ans2 += r switch
                {
                    'X' => (a+2)%3 + 1,
                    'Y' => a + 4,
                    'Z' => (a+1)%3 + 7
                };
            }
            Ans(ans2, 2);
            #endregion
        }
    }
}

