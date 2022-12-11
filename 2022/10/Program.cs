using System;

namespace AoC._2022._10
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2022, 10);

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1

            var grid = new string[6] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
            var X = 1;
            var cycles = 0;
            var signal = 0L;
            foreach (var line in input)
            {
                switch (line[..4])
                {
                    case "addx":
                        signal += CheckSignal(++cycles, X, grid);
                        signal += CheckSignal(++cycles, X, grid);
                        X += int.Parse(line[5..]);
                        break;
                    case "noop":
                        signal += CheckSignal(++cycles, X, grid);
                        break;
                }
            }
            Ans(signal);
            #endregion Part 1

            #region Part 2

            foreach (var line in grid)
                WL(line);
            #endregion
        }

        static int CheckSignal(int cycles, int X, string[] g)
        {
            var p = cycles - 1;
            g[p / 40] += Math.Abs(X - (p % 40)) <= 1 ? '#' : ' ';
            return cycles % 40 == 20 ? cycles * X : 0;
        }
    }
}

