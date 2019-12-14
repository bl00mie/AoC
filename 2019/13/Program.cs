using System;
using System.Collections.Generic;
using System.Linq;
using AoC.VM;
using AoC.VM.IntCode;

namespace AoC._2019._13
{
    class Program : ProgramBase
    {

        static void Main()
        {
            #region input

            var input = AoCUtil.GetAocInput(2019, 13).First().GetLongs().ToArray();
            #endregion

            #region Part 1
            var vm = new VM_2019<GameIO>(input)
            {
                IO = new GameIO()
            };

            vm.Go();
            Ans(vm.IO.blockCount);
            #endregion Part 1

            #region Part 2

            vm.Mem[0] = 2;
            vm.Go();
            //Ans(vm.IO.score, 2);
            #endregion
        }
    }

    public class GameIO : IOContext
    {
        public int blockCount;
        char[,] grid = new char[35, 21];
        public List<long> outputs = new List<long>();
        public long score;
        long ballX = 0;
        long paddleX = 0;
        public bool play;

        public long Input()
        {
            if (play)
            {
                var key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case 'j': return -1;
                    case 'l': return 1;
                    case 'k': return 0;
                }
            }
            else {
                if (ballX > paddleX) return 1;
                if (ballX < paddleX) return -1;
            }
            return 0;
        }

        public void Output(long val)
        {
            outputs.Add(val);
            if (outputs.Count == 3)
            {
                if (outputs[0] == -1 && outputs[1] == 0) score = val;
                else
                {
                    if (outputs[2] == 2) blockCount++;
                    switch (outputs[2])
                    {
                        case 0:
                            grid[outputs[0], outputs[1]] = ' ';
                            break;
                        case 1:
                            grid[outputs[0], outputs[1]] = '\\';
                            break;
                        case 2:
                            grid[outputs[0], outputs[1]] = '0';
                            break;
                        case 3:
                            grid[outputs[0], outputs[1]] = '-';
                            paddleX = outputs[0];
                            break;
                        case 4:
                            grid[outputs[0], outputs[1]] = '*';
                            ballX = outputs[0];
                            break;
                    }
                }
                drawGrid();
                outputs.Clear();
            }
        }

        void drawGrid()
        {
            Console.WriteLine("Score: {0}", score);
            for (int y = 0; y<21; y++)
            {
                for (int x = 0; x < 35; x++)
                    Console.Write(grid[x, y]);
                Console.Write('\n');
            }
        }

        public void Reset()
        {
            outputs.Clear();
        }
    }
}

