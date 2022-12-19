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
            var input = AoCUtil.GetAocInput(2022, 4).Extract<((int a, int b) a, (int a, int b) b)>(@"((\d+)-(\d+)),((\d+)-(\d+))").ToList();
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            Ans(input.Count(t => t.a.Encloses(t.b) || t.b.Encloses(t.a)));
            #endregion Part 1

            #region Part 2
            Ans2(input.Count(t => t.a.Overlaps(t.b)));
            #endregion
        }
    }
}

