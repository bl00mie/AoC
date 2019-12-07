using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2015._18
{
    class Program
    {
        const int MAX = 100;
        static char[][] grid;
        static char[][] newGrid = InitGrid(MAX, false);

        static void Main()
        {
            var input = AoCUtil.GetAocInput(2015, 18).ToArray();
            grid = input.Select(x => x.ToCharArray()).ToArray();
            //PrintGrid();

            #region Part 1

            for (int i = 0; i < 100; i++)
            {
                for (int x = 0; x < MAX; x++)
                    for (int y = 0; y < MAX; y++)
                    {
                        togl(x, y);
                    }
                grid = newGrid;
                newGrid = InitGrid(MAX, false);
            }
            //Console.WriteLine(PrintGrid());

            #endregion Part 1


            #region Part 2
            grid = input.Select(x => x.ToCharArray()).ToArray();
            grid[0][0] = '#';
            grid[0][MAX - 1] = '#';
            grid[MAX - 1][0] = '#';
            grid[MAX - 1][MAX - 1] = '#';
            Console.WriteLine(PrintGrid());

            for (int i = 0; i < 100; i++)
            {
                for (int x = 0; x < MAX; x++)
                    for (int y = 0; y < MAX; y++)
                    {
                        if (IsCorner(x, y)) continue;
                        togl(x, y);
                    }
                grid = newGrid;
                newGrid = InitGrid(MAX, true);
            }
            Console.WriteLine(PrintGrid());


            #endregion
        }

        static bool IsCorner(int x, int y)
        {
            if (x == 0 || x == MAX - 1)
            {
                if (y == 0 || y == MAX - 1)
                    return true;
            }
            return false;
        }

        static char[][] InitGrid(int max, bool corners)
        {
            var g = new char[max][];
            for (int i = 0; i < max; i++)
            {
                g[i] = new char[max];
            }
            g[0][0] = '#';
            g[0][MAX - 1] = '#';
            g[MAX - 1][0] = '#';
            g[MAX - 1][MAX - 1] = '#';

            return g;
        }

        static int PrintGrid()
        {
            int on = 0;
            Console.WriteLine("-------");
            foreach (var row in grid)
            {
                foreach (var light in row)
                {
                    Console.Write(light);
                    if (light == '#') on++;
                }
                Console.Write("\n");
            }
            return on;
        }

        static int CountAdjacent(int x, int y)
        {
            int tot = 0;
            for (int i = Math.Max(x-1, 0); i < Math.Min(MAX,Math.Max(0,x+2)); i++)
            {
                for (int j = Math.Max(y-1, 0); j < Math.Min(MAX, Math.Max(0, y+2)); j++)
                {
                    if (i == x && j == y) continue;
                    if (grid[i][j] == '#') tot++;
                }
            }
            return tot;
        }
        static void togl(int x, int y)
        {
            int adj = CountAdjacent(x, y);
            if (grid[x][y] == '#')
            {
                if (adj != 2 && adj != 3)
                    newGrid[x][y] = '.';
                else
                    newGrid[x][y] = '#';
            }
            else
            {
                if (adj == 3)
                    newGrid[x][y] = '#';
                else
                    newGrid[x][y] = '.';
            }
        }
    }
}
