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
            var inputGroups = AoCUtil.GroupInput(2022, 5);
            var top = inputGroups.First().ToArray();
            var moves = inputGroups.Last().Extract<(int count, int from, int to)>(@"move (\d+) from (\d+) to (\d+)").ToArray();

            var crates = ParseCrates(top);
            var crates2 = ParseCrates(top);
            foreach (var (count, from, to) in moves)
            {
                crates[to - 1].Push(crates[from - 1].Pop(count));
                crates2[to - 1].Push(crates2[from - 1].Pop(count).Reverse());
            }            
            Ans(string.Join("", crates.Select(col => col.Peek())));
            Ans2(string.Join("", crates2.Select(col=>col.Peek())));
        }

        static Stack<char>[] ParseCrates(string[] input)
        {
            var crates = Enumerable.Range(0, 9).Select(_ => new Stack<char>()).ToArray();
            for (int i = 0; i < 9; i++)
                for (int j = input.Length - 2; j >= 0; j--)
                    if (input[j][4 * i + 1] != ' ') 
                       crates[i].Push(input[j][4 * i + 1]);
            return crates;
        }
    }
}

