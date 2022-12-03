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
            var ans = 0;
            foreach (var rs in input)
                foreach (var c in rs[..(rs.Length / 2)])
                    if (rs[(rs.Length / 2)..].Contains(c))
                    {
                        ans += Priority(c);
                        break;
                    }
            Ans(ans);
            #endregion Part 1

            #region Part 2
            var ans2 = 0;
            foreach (var g in input.Chunk(3))
                foreach (char c in g[0])
                    if (g[1].Contains(c) && g[2].Contains(c))
                    {
                        ans2 += Priority(c);
                        break;
                    }
            Ans2(ans2);
            #endregion
        }

        static int Priority(char c)
        {
            if (c >= 'a' && c <= 'z')
                return c - 'a' + 1;
            return c - 'A' + 27;
        }
    }
}

