using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2015._23
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input

            List<(string, int, int)> instructions = new List<(string, int, int)>();
            foreach (string[] sa in AoCUtil.GetAocInput(2015, 23).Select(L => L.Split(' ')))
            {
                int r = 0;
                int off = 0;
                if (sa[0][0] == 'j')
                {
                    if (sa[0][2] == 'p')
                    {
                        off = int.Parse(sa[1].Replace("+", ""));
                    }
                    else
                    {
                        r = sa[1][0] - 'a';
                        off = int.Parse(sa[2].Replace("+", ""));
                    }
                }
                else
                {
                    r = sa[1][0] - 'a';
                }
                instructions.Add((sa[0], r, off));
            }

            #endregion

            #region Part 1

            VM_2015 vm = new VM_2015(instructions);
            vm.Go();

            Ans(vm.Regs[1]);

            #endregion Part 1

            #region Part 2
            vm.Reset();
            vm.Regs[0] = 1;
            vm.Go();
            Ans(vm.Regs[1], 2);
            #endregion
        }
    }

    public class VM_2015
    {
        public int P { get; set; } = 0;
        public List<(string cmd, int reg, int offset)> Ins { get; set; }
        public int[] Regs = new int[2];

        public Dictionary<string, CmdF> Cmds;

        public bool PauseOnOutput;
        public bool Debug;

        public VM_2015(List<(string, int, int)> ins)
        {
            Ins = ins;
            Cmds = new Dictionary<string, CmdF>()
            {
                { "hlf", (ref int r, int off) => { r /= 2; return 1; } },
                { "tpl", (ref int r, int off) => { r *= 3; return 1; } },
                { "inc", (ref int r, int off) => { r++; return 1; } },
                { "jmp", (ref int r, int off) => { return off; } },
                { "jie", (ref int r, int off) => { return r % 2 == 0 ? off : 1; } },
                { "jio", (ref int r, int off) => { return r == 1 ? off : 1; } }
            };
        }

        public void Reset()
        {
            P = 0;
            Regs[0] = 0;
            Regs[1] = 0;
        }

        public static void PrintCmd((string, int, int) cmd)
        {
            Console.WriteLine(string.Format("{0} {1} {2}", cmd.Item1, cmd.Item2, cmd.Item3));
        }

        public void PrintCmds()
        {
            for (int i=0; i<Ins.Count; i++)
            {

                Console.WriteLine("{0}[{1:D4}] {2,-4} {3,-4} {4,-4}", i == P ? "->" : "  ", i, Ins[i].cmd, Ins[i].reg, Ins[i].offset);
            }
        }

        public void PrintRegs()
        {
            Console.WriteLine(string.Format("r[{0}]: {1}\nr[{2}]: {3}", 0, Regs[0], 1, Regs[1]));
        }

        public int Go()
        {
            while (P >= 0 && P < Ins.Count)
            {
                if (Debug)
                {
                    PrintCmds();
                    Console.WriteLine("P: {0} R[a]: {1} R[b]: {2}", P, Regs[0], Regs[1]);
                }
                var ins = Ins[P];
                P += Cmds[ins.cmd](ref Regs[ins.reg], ins.offset);
                if (Debug)
                {
                    Console.WriteLine("P: {0} R[a]: {1} R[b]: {2}", P, Regs[0], Regs[1]);
                }
            }
            return 0;
        }
    }

    public delegate int CmdF(ref int r, int off);

}
