﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoCUtil.Collections;

namespace AoC._2021._20
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GroupInput(2021, 20);
            var alg = input.First().First();
            DefaultDictionary<Coord, char> grid = new DefaultDictionary<Coord, char>('.');
            var img = input.Last().ToList();
            for (int y = 0; y < img.Count; y++)
                for (int x = 0; x < img[0].Length; x++)
                    grid[new(x, y)] = img[y][x];
            var L = 0;
            var R = img[0].Length;
            var T = 0;
            var B = img.Count;
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            var nextGrid = new DefaultDictionary<Coord, char>() { def = '#' };
            for (int i = 0; i < 50; i++)
            {
                L--; R++;
                T--; B++;
                for (int y = T; y < B; y++)
                    for (int x = L; x < R; x++)
                    {
                        int index = 0;
                        for (int dy = -1; dy < 2; dy++)
                            for (int dx = -1; dx < 2; dx++)
                            {
                                index <<= 1;
                                index += grid[new(x + dx, y + dy)] == '.' ? 0 : 1;
                            }
                        nextGrid[new(x,y)] = alg[index];
                    }
                swap(ref grid, ref nextGrid);
                nextGrid.Clear();
                if (i == 1)
                    Ans(grid.Where(entry => entry.Value == '#').Count());
            }
            Ans2(grid.Where(entry => entry.Value == '#').Count());
        }

        static void swap(ref DefaultDictionary<Coord, char> a, ref DefaultDictionary<Coord, char> b)
        {
            DefaultDictionary<Coord, char> temp = a;
            a = b;
            b = temp;
        }

        static void draw(Dictionary<Coord, char> grid, int l, int r, int t, int b)
        {
            var sb = new StringBuilder();
            for (int y = t; y < b; y++)
            {
                for (int x = l; x < r; x++)
                    sb.Append(grid[new(x, y)]);
                sb.AppendLine();
            }
            WL(sb);
        }
    }
}

