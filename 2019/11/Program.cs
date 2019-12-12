using System;
using System.Collections.Generic;
using System.Linq;
using AoC.VM;
using AoC.VM.IntCode;

namespace AoC._2019._11
{
    class Program : ProgramBase
    {
        static int x, y;
        static Dictionary<(long x, long y), long> painted = new Dictionary<(long x, long y), long>();
        static List<(int x, int y)> dirs = new List<(int x, int y)>()
        {
            { (0, 1) },
            { (1, 0) },
            { (0, -1) },
            { (-1, 0) }
        };
        static int minx = int.MaxValue, maxx = int.MinValue;
        static int miny = int.MaxValue, maxy = int.MinValue;
        static void Main()
        {
            #region input
            
            var input = AoCUtil.GetAocInput(2019, 11).First().Split(',').Select(long.Parse).ToArray();
            VM_2019<RobotIO> vm = new VM_2019<RobotIO>(input)
            {
                IO = new RobotIO()
            };

            #endregion

            #region Part 1
            vm.Go();
            Ans(painted.Count);
            #endregion Part 1

            #region Part 2

            int dy = maxy - miny;
            int dx = maxx - minx;
            char[,] grid = new char[dx+1, dy+1];
            for (int i = 0; i <= dx; i++)
                for (int j = 0; j <= dy; j++)
                {
                    grid[i, j] = ' ';
                }
            Ans("", 2);

            foreach (var square in painted)
            {
                if (square.Value == 0) continue;
                grid[square.Key.x - minx, square.Key.y - miny] = '#';
            }
            for (int j = dy; j >= 0; j--)
            {
                for (int i = 0; i <= dx; i++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.Write('\n');
            }
            #endregion
        }

        public class RobotIO : IOContext
        {
            bool step;
            int dir;
            bool first = true;

            public long Input()
            {
                if (first)
                {
                    first = false;
                    return 1;
                }
                return painted.ContainsKey((x, y)) ? painted[(x, y)] : 0;
            }

            public void Output(long val)
            {
                if (step = !step)
                {
                    painted[(x, y)] = val;
                }
                else
                {
                    dir = ((dir + (val == 1 ? 1 : -1)) + 4) % 4;
                    x += dirs[dir].x;
                    y += dirs[dir].y;
                    if (x < minx) minx = x;
                    if (x > maxx) maxx = x;
                    if (y < miny) miny = y;
                    if (y > maxy) maxy = y;
                }
            }

            public void Reset()
            {
                step = false;
                dir = 0;
                first = true;
            }
        }
    }
}

