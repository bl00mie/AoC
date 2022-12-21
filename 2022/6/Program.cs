using System.Linq;

namespace AoC._2022._6
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            var input = AoCUtil.GetAocInput(2022, 6).Single();
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            Ans(Find(input));
            #endregion Part 1

            #region Part 2
            Ans2(Find(input, 14));
            #endregion
        }

        static int Find(string input, int sz=4)
        {
            var p = sz;
            while (input[(p - sz)..p++].Distinct().Count() != sz) ;
            return p - 1;
        }
    }
}

