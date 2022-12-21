using System.Collections.Generic;
using System.Linq;

namespace AoC._2022._7
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            var input = AoCUtil.GetAocInput(2022, 7).Select(l => l.Split(' '));
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var dirs = new AoCDictionary<string, int>(0, true);
            var path = new List<string>();
            foreach (var cmd in input)
                if (cmd[1] == "ls" || cmd[0] == "dir") continue;
                else if (cmd[1] == "cd")
                    if (cmd[2][0] == '.') path.RemoveAt(path.Count - 1);
                    else path.Add(cmd[2]);
                else
                {
                    int size = int.Parse(cmd[0]);
                    for (int i = 1; i <= path.Count; i++)
                        dirs[string.Join('/', path.GetRange(0,i))] += size;
                }
            Ans(dirs.Values.Where(size => size <= 100_000).Sum());
            #endregion Part 1

            #region Part 2
            var neededSpace = dirs["/"] - 40_000_000;
            Ans2(dirs.Values.Where(sz => sz >= neededSpace).Min());
            #endregion
        }
    }
}

