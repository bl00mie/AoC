using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2019._03
{
    class Program
    {
        const int S = 20000;
        static void Main(string[] args)
        {
            #region Template

            var lines = AoCUtil.ReadLines("../../../input.txt").ToArray();

            #endregion Template


            #region Problem

            bool[,] grid = new bool[S,S];
            int px = S/2, py = px;

            foreach (string val in lines[0].Split(","))
            {
                draw(grid, ref px, ref py, val, true);
            }
            px = S/2;
            py = px;
            List<Tuple<int, int>> crosses = new List<Tuple<int, int>>();
            foreach (string val in lines[1].Split(","))
            {
                crosses.AddRange(draw(grid, ref px, ref py, val, false));
            }

            int minD = int.MaxValue;
            foreach (Tuple<int,int> t in crosses)
            {
                int md = Math.Abs(t.Item1 - 10000) + Math.Abs(t.Item2 - 10000);
                if (md < minD) minD = md;
            }
            Console.WriteLine(minD);
            #endregion
        }

        static List<Tuple<int,int>> draw(bool[,] grid, ref int px, ref int py, string move, bool mark)
        {
            List<Tuple<int, int>> crosses = new List<Tuple<int, int>>();
            int d = int.Parse(move.Substring(1));
            int dir = move[0] == 'R' || move[0] == 'U' ? 1 : -1;
            ref int p = ref move[0] == 'R' || move[0] == 'L' ? ref px : ref py;
            for (int i=0; i<d; i++)
            {
                p += dir;
                if (mark)
                {
                    grid[px, py] = true;
                }
                else if (grid[px, py]) 
                {
                    crosses.Add(new Tuple<int, int>(px, py));
                }
            }
            return crosses;
        }
    }
}
