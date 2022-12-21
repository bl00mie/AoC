using System.Linq;

namespace AoC._2022._1
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            var elves = AoCUtil.GroupInput(2022, 1)
                .Select(g => g.Sum(l => int.Parse(l)));
            var sorted = elves.OrderByDescending(x => x).ToArray();
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion

            Ans(sorted[0]);
            Ans2(sorted[..3].Sum());
        }
    }
}

