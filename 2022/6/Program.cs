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
            Ans(FindSequence(input));
            #endregion Part 1

            #region Part 2
            Ans2(FindSequence(input, 14));
            #endregion
        }

        static int FindSequence(string input, int size=4)
        {
            var ans = size;
            while (true)
                if (input[(ans - size)..ans++].ToHashSet().Count == size)
                    break;
            return ans - 1;
        }
    }
}

