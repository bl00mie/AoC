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

            int[,] grid = new int[S,S];
            int px = S/2, py = px;
            int D = 0;

            foreach (string val in lines[0].Split(","))
            {
                draw(grid, ref px, ref py, ref D, val, true);
            }
            px = S/2;
            py = px;
            D = 0;
            List<Tuple<int, int, int>> crosses = new List<Tuple<int, int,int>>();
            foreach (string val in lines[1].Split(","))
            {
                crosses.AddRange(draw(grid, ref px, ref py, ref D, val, false));
            }

            int minD = int.MaxValue;
            foreach (Tuple<int,int,int> t in crosses)
            {
                //int md = Math.Abs(t.Item1 - 10000) + Math.Abs(t.Item2 - 10000);
                //if (md < minD)
                //{
                //    minMD = md;
                //}
                if (t.Item3 < minD) minD = t.Item3;
            }
            Console.WriteLine(minD);
            #endregion
        }

        static List<Tuple<int,int,int>> draw(int[,] grid, ref int px, ref int py, ref int D, string move, bool mark)
        {
            List<Tuple<int, int, int>> crosses = new List<Tuple<int, int, int>>();
            int d = int.Parse(move.Substring(1));
            int dir = move[0] == 'R' || move[0] == 'U' ? 1 : -1;
            ref int p = ref move[0] == 'R' || move[0] == 'L' ? ref px : ref py;
            for (int i=0; i<d; i++)
            {
                p += dir;
                D++;
                if (mark)
                {
                    grid[px, py] = D;
                }
                else if (grid[px, py] != 0) 
                {
                    crosses.Add(new Tuple<int, int, int>(px, py, D + grid[px,py]));
                }
            }
            return crosses;
        }
    }
}
