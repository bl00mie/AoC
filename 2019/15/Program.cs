using System;
using System.Collections.Generic;
using System.Linq;
using AoC.VM;
using AoC.VM.IntCode;

namespace AoC._2019._15
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input

            var input = AoCUtil.GetAocInput(2019, 15).First().GetLongs().ToArray();

            #endregion

            #region Part 1
            VM_2019<RobotIO> vm = new VM_2019<RobotIO>(input)
            {
                IO = new RobotIO()
            };
            vm.IO.Reset();
            vm.Go();
            Ans(vm.IO.min);
            #endregion Part 1

            #region Part 2

            Ans("", 2);
            #endregion
        }
    }

    class RobotIO : IOContext
    {
        readonly Dictionary<int, (int DX, int DY)> dxy = new Dictionary<int, (int DX, int DY)>()
        {
            {1, (0,1) },
            {2, (1,0) },
            {3, (0,-1) },
            {4, (-1, 0) }
        };
        long x, y;

        public long min = long.MaxValue;
        readonly Stack<int> moves = new Stack<int>();
        readonly ISet<(long x, long y)> visited = new HashSet<(long x, long y)>();
        readonly Queue<int> backtrack = new Queue<int>();


        public RobotIO()
        {
            Reset();
        }


        public long Input()
        {
            if (backtrack.Count > 0)
            {
                return backtrack.Peek();
            }
            return moves.Peek();
        }

        public void Output(long val)
        {
            if (backtrack.Count > 0)
            {
                backtrack.Dequeue();
                return;
            }
            int next;
            switch(val)
            {
                case 0:
                    next = nextMove(moves.Peek());
                    if (next == 0)
                    {
                        moves.Pop();
                        backtrack.Enqueue((moves.Peek() + 2) % 4);
                    }
                    else
                        moves.Push(next);
                    break;
                case 1:
                    x += dxy[moves.Peek()].DX;
                    y += dxy[moves.Peek()].DY;
                    visited.Add((x, y));
                    next = nextMove();
                    if (next == 0)
                        backtrack.Enqueue(moves.Pop() + 2 % 4);
                    else moves.Push(next);
                    break;
                case 2:
                    var moveCount = moves.Count;
                    if (moveCount < min)
                    {
                        backtrack.Enqueue((moves.Pop() + 2) % 4);
                    }
                    break;
            }
        }

        int nextMove(int last = 0)
        {
            int next = last + 1;
            while (next <= 4)
            {
                if (visited.Contains((x + dxy[next].DX, y + dxy[next].DY)))
                {
                    next++;
                    continue;
                }
            }
            return next < 4 ? next : 0;
        }

        public void Reset()
        {
            moves.Clear();
            moves.Push(1);
            visited.Clear();
            visited.Add((0, 0));
            backtrack.Clear();
            x = 0; y = 0;
        }
    }
}

