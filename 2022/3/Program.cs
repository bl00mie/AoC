using System.Linq;

namespace AoC._2022._3
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region Input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2022, 3).ToList();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            Ans(input.Sum(rs => Priority(rs[..(rs.Length / 2)].Intersect(rs[(rs.Length / 2)..]).Single())));
            #endregion Part 1

            #region Part 2
            Ans2(input.Chunk(3).Sum(g => Priority(g[0].Intersect(g[1]).Intersect(g[2]).Single())));
            #endregion
        }

        static int Priority(char c) => (c >= 'a' && c <= 'z') ? c - 'a' + 1 : c - 'A' + 27;
    }
}

