using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AoC._2015._05
{
    class Program
    {
        delegate void Op(int[,] grid, int x, int y);
        static void Main()
        {
            var input = AoCUtil.GetAocInput(2015, 7).ToArray();

            Dictionary<char, Op> ops = new Dictionary<char, Op>()
            {
                {'n', (grid, x, y) => {grid[x,y] = 1;} },
                {'f', (grid, x, y) => {grid[x,y] = 0;} },
                {'t', (grid, x, y) => {grid[x,y] = grid[x,y] == 1 ? 0 : 1;} }
            };

            #region Part 1

            var grid = new int[1000,1000];
            foreach (var cmd in input)
            {
                char op = cmd[6] != ' ' ? cmd[6] : 't';
                var sa = cmd.Split(' ');
                int[] p1 = sa[^3].Split(',').Select(x => int.Parse(x)).ToArray();
                int[] p2 = sa[^1].Split(',').Select(x => int.Parse(x)).ToArray();
                for (int x = p1[0]; x<=p2[0]; x++)
                {
                    for (int y=p1[1]; y<=p2[1]; y++)
                    {
                        ops[op](grid, x, y);
                    }
                }
            }

            int on = 0;
            for (int x=0; x<1000; x++)
            {
                for (int y=0; y<1000; y++)
                {
                    if (grid[x, y] == 1) on++;
                }
            }
            Console.WriteLine(on);

            #endregion Part 1


            #region Part 2

            ops['n'] = (grid, x, y) => { grid[x, y]++; };
            ops['f'] = (grid, x, y) => { if (grid[x, y] > 0) grid[x, y]--; };
            ops['t'] = (grid, x, y) => { grid[x, y] += 2; };
            grid = new int[1000, 1000];

            foreach (var cmd in input)
            {
                char op = cmd[6] != ' ' ? cmd[6] : 't';
                var sa = cmd.Split(' ');
                int[] p1 = sa[^3].Split(',').Select(x => int.Parse(x)).ToArray();
                int[] p2 = sa[^1].Split(',').Select(x => int.Parse(x)).ToArray();
                for (int x = p1[0]; x <= p2[0]; x++)
                {
                    for (int y = p1[1]; y <= p2[1]; y++)
                    {
                        ops[op](grid, x, y);
                    }
                }
            }
            int bright = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    bright += grid[x, y];
                }
            }
            Console.WriteLine(bright);
            #endregion
        }
    }
}
