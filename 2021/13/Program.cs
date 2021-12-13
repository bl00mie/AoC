using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2021._13
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GroupInput(2021, 13) as List<IEnumerable<string>>;
            var dots  = input[0].Extract<(int x, int y)>(@"(\d+),(\d+)");
            var folds = input[1].Extract<(char axis, int v)>(@"fold along (\w)=(\d+)");
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            foreach (var (axis, v) in folds)
            {
                if (axis == 'x')
                    dots = dots.Select(dot => dot switch {
                        (int x, int y) when x > v => (2 * v - x, y),
                        (int x, int y) => (x, y)
                    }).ToHashSet();
                else
                    dots = dots.Select(dot => dot switch {
                        (int x, int y) when y > v => (x, 2 * v - y),
                        (int x, int y) => (x, y)
                    }).ToHashSet();
                Ans(dots.Count());
            }
            AoCUtil.PaintGrid(dots);
        }
    }
}

