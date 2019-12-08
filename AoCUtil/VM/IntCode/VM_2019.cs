using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.VM.IntCode
{
    public class VM_2019<T> where T : IOContext
    {

        public int P { get; set; } = 0;
        public int[] Mem { get; set; }
        private readonly int[] InitialMem;
        public T IO;
        public Dictionary<OpCode, Op> Ops;
        public bool PauseOnOutput;
        public bool Debug;

        public VM_2019(int[] memory) : this(memory, false) { }
        public VM_2019(int[] memory, bool pauseOnOutput)
        {
            PauseOnOutput = pauseOnOutput;
            InitialMem = (int[])memory.Clone();
            Reset();

            Ops = new[]
            {
                new Op(OpCode.Add, 3, 3,    (a, b, c) => Mem[c] = a + b),
                new Op(OpCode.Mul, 3, 3,    (a, b, c) => Mem[c] = a * b),
                new Op(OpCode.In,  1, 1,    (a, b, c) => Mem[a] = IO.Input()),
                new Op(OpCode.Out, 1, null, (a, b, c) => IO.Output(a)),
                new Op(OpCode.JZ,  2, null, (a, b, c) => P = a == 0 ? b-3 : P),
                new Op(OpCode.JNZ, 2, null, (a, b, c) => P = a != 0 ? b-3 : P),
                new Op(OpCode.LT,  3, 3,    (a, b, c) => Mem[c] = a < b ? 1 : 0),
                new Op(OpCode.EQ,  3, 3,    (a, b, c) => Mem[c] = a == b ? 1 : 0),
                new Op(OpCode.Halt,0, null, (a, b, c) => { })
            }.ToDictionary(o => o.OpCode);
        }

        public void Reset()
        {
            P = 0;
            Mem = (int[])InitialMem.Clone();
        }

        private static (OpCode, Mode[]) ParseOpCode(int opcode)
        {
            var opCode = (OpCode)(opcode % 100);
            string s = opcode.ToString().PadLeft(5, '0');
            return (opCode, new Mode[] {
                    s[2] == '1' ? Mode.Immediate : Mode.Position,
                    s[1] == '1' ? Mode.Immediate : Mode.Position,
                    s[0] == '1' ? Mode.Immediate : Mode.Position });
        }

        private int Param(Mode mode, int num, int? refNum)
        {
            return mode == Mode.Immediate || num == refNum ? Mem[num+P] : Mem[Mem[num+P]];
        }

        private (Op, int, int, int) NextOp()
        {
            (OpCode opcode, Mode[] modes) = ParseOpCode(Mem[P]);
            Op op = Ops[opcode];
            int[] vars = new int[3];
            foreach (int i in Enumerable.Range(0, 3))
            {
                vars[i] = op.Argc > i ? Param(modes[i], i + 1, op.RefI) : -1;
            }

            if (Debug)
            {
                PrintOp(op, vars, modes);
            }

            return (op, vars[0], vars[1], vars[2]);
        }

        private void PrintOp(Op op, int[] vars, Mode[] modes)
        {
            StringBuilder sb = new StringBuilder(30);
            sb.Append(string.Format("[{0,-4:0000}] {1,-6}", P, op.OpCode.ToString()));
            foreach (int i in Enumerable.Range(0, vars.Length))
            {
                if (vars[i] < 0) break;
                var s = (modes[i] == Mode.Position ? "*" : "") + Mem[P + i];
                sb.Append(string.Format("{0,-8}", s));
            }
            Console.WriteLine(sb);
        }

        public int Go()
        {
            while (true)
            {
                (Op op, int a, int b, int c) = NextOp();
                op.Action(a, b, c);
                P += op.Argc + 1;
                if (PauseOnOutput && op.OpCode == OpCode.Out) return 0;
                if (op.OpCode == OpCode.Halt) return -1;
            }
        }
    }
}
