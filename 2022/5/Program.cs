using System;
using System.Collections.Generic;
using System.Linq;
using AoCUtil;
using RegExtract;

namespace AoC._2022._5
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var inputGroups = AoCUtil.GroupInput(2022, 5).ToArray();
            var top = inputGroups[0].ToArray();
            var moves = inputGroups[1].Extract<(int count, int from, int to)>(@"move (\d+) from (\d+) to (\d+)");

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var crates = ParseCrates(top);
            foreach (var (count, from, to) in moves)
                crates[to-1].Push(crates[from-1].Pop(count));
            
            Ans(new string(crates.Select(col => col.Peek()).ToArray()));
            #endregion Part 1

            #region Part 2
            crates = ParseCrates(top);
            foreach (var (count, from, to) in moves)
                crates[to-1].Push(crates[from-1].Pop(count).Reverse());

            Ans2(new string(crates.Select(col=>col.Peek()).ToArray()));
            #endregion
        }

        static List<Stack<char>> ParseCrates(string[] input)
        {
            var crates = new List<Stack<char>> { new(), new(), new(), new(), new(), new(), new(), new(), new()};
            for (int i = 0; i < 9; i++)
                for (int j = input.Length - 2; j >= 0; j--)
                    if (input[j][4 * i + 1] != ' ') 
                       crates[i].Push(input[j][4 * i + 1]);
            return crates;
        }
    }
}

