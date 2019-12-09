using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.VM.IntCode
{
    public class VM_2019<T> where T : IOContext
    {
        private readonly long[] InitialMem;

        public long P { get; set; } = 0;
        public long[] Mem { get; set; }
        public long RB { get; set; } = 0;

        public T IO;
        public Dictionary<OpCode, Op> Ops;

        public bool PauseOnOutput;
        public bool Debug;

        public VM_2019(long[] memory) : this(memory, false) { }
        public VM_2019(long[] memory, bool pauseOnOutput)
        {
            PauseOnOutput = pauseOnOutput;
            InitialMem = new long[1000000];
            Array.Copy(memory, InitialMem, memory.Length);
            Reset();

            Ops = new[]
            {
                new Op(OpCode.Add, 3, (ref long a, ref long b, ref long c) => { c = a + b; return 3; }),
                new Op(OpCode.Mul, 3, (ref long a, ref long b, ref long c) => { c = a * b; return 3; }),
                new Op(OpCode.In,  1, (ref long a, ref long b, ref long c) => { a = IO.Input(); return 1; }),
                new Op(OpCode.Out, 1, (ref long a, ref long b, ref long c) => { IO.Output(a); return 1; }),
                new Op(OpCode.JZ,  2, (ref long a, ref long b, ref long c) => { P = a == 0 ? b-3 : P; return 2; }),
                new Op(OpCode.JNZ, 2, (ref long a, ref long b, ref long c) => { P = a != 0 ? b-3 : P; return 2; }),
                new Op(OpCode.LT,  3, (ref long a, ref long b, ref long c) => { c = a < b ? 1 : 0; return 3; }),
                new Op(OpCode.EQ,  3, (ref long a, ref long b, ref long c) => { c = a == b ? 1 : 0; return 3; }),
                new Op(OpCode.SRB, 1, (ref long a, ref long b, ref long c) => { RB += a; return 1; }),
                new Op(OpCode.Halt,0, (ref long a, ref long b, ref long c) => { return -1; })
            }.ToDictionary(o => o.OpCode);
        }

        public void Reset()
        {
            P = 0;
            RB = 0;
            Mem = (long[])InitialMem.Clone();
        }

        private static (OpCode, Mode[]) ParseOpCode(long opcode)
        {
            long m = opcode / 100;
            return ((OpCode)(opcode % 100), new Mode[] {
                    (Mode)(m%10),
                    (Mode)(m/10%10),
                    (Mode)(m/100) });
        }

        private void PrintOp(Op op, long[] vars, Mode[] modes)
        {
            StringBuilder sb = new StringBuilder(30);
            sb.Append(string.Format("[{0,-4:0000}] {1,-6}", P, op.OpCode.ToString()));
            foreach (int i in Enumerable.Range(0, vars.Length))
            {
                if (vars[i] < 0) break;
                var s = (modes[i] == Mode.Position ? "*" : "") + vars[i];
                sb.Append(string.Format("{0,-8}", s));
            }
            Console.WriteLine(sb);
        }

        public int Go()
        {
            long garbage = 0;
            while (true)
            {
                ref long refa = ref garbage, refb = ref garbage, refc = ref garbage;

                (OpCode opcode, Mode[] modes) = ParseOpCode(Mem[P]);
                var op = Ops[opcode];

                if (op.Argc > 0)
                    if (modes[0] == Mode.Immediate) refa = ref Mem[P + 1];
                    else refa = ref Mem[Mem[P + 1] + (modes[0] == Mode.Relative ? RB : 0)];
                if (op.Argc > 1)
                    if (modes[1] == Mode.Immediate) refb = ref Mem[P + 2];
                    else refb = ref Mem[Mem[P + 2]+ (modes[1] == Mode.Relative ? RB : 0)];
                if (op.Argc > 2)
                    if (modes[2] == Mode.Immediate) refc = ref Mem[P + 3];
                    else refc = ref Mem[Mem[P + 3] + (modes[2] == Mode.Relative ? RB : 0)];

                if (Debug) PrintOp(op, new long[] { refa, refb, refc }, modes);

                op.F(ref refa, ref refb, ref refc);
                P += op.Argc + 1;
                if (PauseOnOutput && op.OpCode == OpCode.Out) return 0;
                if (op.OpCode == OpCode.Halt) return -1;
            }
        }
    }
}
