using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.VM.IntCode
{
    public class VM_2019<T> where T : IOContext
    {

        public int p { get; set; } = 0;
        public int[] mem { get; set; }
        private int[] initialMem { get; set; }
        public T io;
        public Dictionary<OpCode, Op> Ops;
        public bool pauseOnOutput;
        public bool debug = true;

        public VM_2019(int[] memory) : this(memory, false) { }
        public VM_2019(int[] memory, bool pauseOnOutput)
        {
            this.pauseOnOutput = pauseOnOutput;
            initialMem = (int[])memory.Clone();
            Reset();

            this.Ops = new[]
            {
                new Op(OpCode.Add, 3, 3,    (a, b, c) => this.mem[c] = a + b),
                new Op(OpCode.Mul, 3, 3,    (a, b, c) => this.mem[c] = a * b),
                new Op(OpCode.In,  1, 1,    (a, b, c) => this.mem[a] = this.io.input()),
                new Op(OpCode.Out, 1, null, (a, b, c) => this.io.output(a)),
                new Op(OpCode.JZ,  2, null, (a, b, c) => this.p = a == 0 ? b-3 : this.p),
                new Op(OpCode.JNZ, 2, null, (a, b, c) => this.p = a != 0 ? b-3 : this.p),
                new Op(OpCode.LT,  3, 3,    (a, b, c) => this.mem[c] = a < b ? 1 : 0),
                new Op(OpCode.EQ,  3, 3,    (a, b, c) => this.mem[c] = a == b ? 1 : 0),
                new Op(OpCode.Halt,0, null, (a, b, c) => { })
            }.ToDictionary(o => o.opCode);
        }

        public void Reset()
        {
            p = 0;
            mem = (int[])initialMem.Clone();
        }

        private static (OpCode, Mode[]) parseOpCode(int opcode)
        {
            var opCode = (OpCode)(opcode % 100);
            string s = opcode.ToString().PadLeft(5, '0');
            return ((OpCode)opCode, new Mode[] {
                    s[2] == '1' ? Mode.Immediate : Mode.Position,
                    s[1] == '1' ? Mode.Immediate : Mode.Position,
                    s[0] == '1' ? Mode.Immediate : Mode.Position });
        }

        private int Param(Mode mode, int num, int? refNum)
        {
            return mode == Mode.Immediate || num == refNum ? mem[num+p] : mem[mem[num+p]];
        }

        private (Op, int, int, int) NextOp()
        {
            (OpCode opcode, Mode[] modes) = parseOpCode(mem[p]);
            Op op = Ops[opcode];
            int[] vars = new int[3];
            foreach (int i in Enumerable.Range(0, 3))
            {
                vars[i] = op.argc > i ? Param(modes[i], i + 1, op.refI) : -1;
            }

            if (debug)
            {
                PrintOp(op, vars, modes);
            }

            return (op, vars[0], vars[1], vars[2]);
        }

        private void PrintOp(Op op, int[] vars, Mode[] modes)
        {
            StringBuilder sb = new StringBuilder(30);
            sb.Append(string.Format("[{0,-4:0000}] {1,-6}", p, op.opCode.ToString()));
            foreach (int i in Enumerable.Range(0, vars.Length))
            {
                if (vars[i] < 0) break;
                var s = (modes[i] == Mode.Position ? "*" : "") + mem[p + i];
                sb.Append(string.Format("{0,-16}", s));
            }
            Console.WriteLine(sb.ToString());
        }

        public int Go()
        {
            while (true)
            {
                (Op op, int a, int b, int c) = NextOp();
                op.action(a, b, c);
                p += op.argc + 1;
                if (pauseOnOutput && op.opCode == OpCode.Out) return 0;
                if (op.opCode == OpCode.Halt) return -1;
            }
        }
    }
}
