using System.Linq;
using RegExtract;

namespace AoC._2022._4
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            var input = AoCUtil.GetAocInput(2022, 4).Extract<(int a1, int a2, int b1, int b2)>(@"(\d+)-(\d+),(\d+)-(\d+)").ToList();
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion
            
            #region Part 1
            Ans(input.Count(t => 
                (t.a1 <= t.b1 && t.a2 >= t.b2) || 
                (t.b1 <= t.a1 && t.b2 >= t.a2)));
            #endregion Part 1

            #region Part 2
            Ans2(input.Count(t =>
                (t.a1 <= t.b1 && t.a2 >= t.b1) ||
                (t.a1 <= t.b2 && t.a2 >= t.b2) ||
                (t.b1 <= t.a1 && t.b2 >= t.a1) ||
                (t.b1 <= t.a2 && t.b2 >= t.a2)));
            #endregion
        }
    }
}

