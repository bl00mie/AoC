using System;
using System.Collections.Generic;

namespace AoC.VM
{
    public class VM_2019
    {
        public enum OpCode
        {
            Add = 1,
            Mul = 2,
            In = 3,
            Out = 4,
            JNZ = 5,
            JZ = 6,
            JLT = 7,
            JEQ = 8
        }

        public int p { get; set; } = 0;
        public int[] mem { get; set; }
        private static int[] initialMem { get; set; }
        public IO io;
        public bool pauseOnOutput = false;

        public Dictionary<int, Func<int[], VM_2019, int>> ops { get; set; } = new Dictionary<int, Func<int[], VM_2019, int>>()
        {
            { // add
                1, (mode, vm) => {
                    var v1 = Param(mode, 1, vm);
                    var v2 = Param(mode, 2, vm);
                    vm.mem[vm.mem[vm.p+3]] = v1 + v2;
                    return vm.p+4;
                }
            },
            { // multiply
                2, (mode, vm) => {
                    var v1 = Param(mode, 1, vm);
                    var v2 = Param(mode, 2, vm);
                    vm.mem[vm.mem[vm.p+3]] = v1 * v2;
                    return vm.p+4;
                }
            },
            { // store input
                3, (mode, vm) => {
                    vm.mem[vm.mem[vm.p + 1]] = vm.io.input();
                    return vm.p+2;
                }
            },
            { // write output
                4, (mode, vm) => {
                    vm.io.output(Param(mode, 1, vm));
                    return vm.p+2;
                }
            },
            { // jump if not zero
                5, (mode, vm) => {
                    var v1 = Param(mode, 1, vm);
                    var v2 = Param(mode, 2, vm);
                    return v1 == 0 ? vm.p + 3 : v2;
                }
            },
            { // jump if zero
                6, (mode, vm) => {
                    var v1 = Param(mode, 1, vm);
                    var v2 = Param(mode, 2, vm);
                    return v1 == 0 ? v2 : vm.p + 3;
                }
            },
            { // less than
                7, (mode, vm) => {
                    var v1 = Param(mode, 1, vm);
                    var v2 = Param(mode, 2, vm);
                    vm.mem[vm.mem[vm.p+3]] = (v1 < v2) ? 1 : 0;
                    return vm.p+4;
                }
            },
            { // equal to
                8, (mode, vm) => {
                    var v1 = Param(mode, 1, vm);
                    var v2 = Param(mode, 2, vm);
                    vm.mem[vm.mem[vm.p+3]] = (v1 == v2) ? 1 : 0;
                    return vm.p+4;
                }
            },
            {
                99, (mode, vm) =>
                {
                    return -1;
                }
            }
        };

        public VM_2019() { }

        public VM_2019(int[] memory) : this(memory, false)
        {
        }

        public VM_2019(int[] memory, bool pauseOnOutput)
        {
            this.pauseOnOutput = pauseOnOutput;
            initialMem = (int[])memory.Clone();
            Reset();
        }

        public static int Param(int[] mode, int pos, VM_2019 vm)
        {
            return vm.mem[mode[mode.Length - pos] == 0 ? vm.mem[vm.p + pos] : vm.p + pos];
        }

        public void Reset()
        {
            p = 0;
            mem = (int[])initialMem.Clone();
        }


        public int Go()
        {
            while (p >= 0)
            {
                var op = mem[p] % 100;
                var modeI = mem[p] / 100;
                var mode = new int[] { modeI / 100, modeI % 100 / 10, modeI % 10 };
                p = ops[op](mode, this);
                if (pauseOnOutput && op == 4) return 0;
            }
            return p;
        }
    }

    public interface IO
    {
        void output(int val);
        int input();
    }

    public class ConstInputIO : IO
    {
        public int inVal;
        public Queue<int> outputs = new Queue<int>();

        public ConstInputIO(int val)
        {
            inVal = val;
        }

        public int input()
        {
            return inVal;
        }

        public void output(int val)
        {
            outputs.Enqueue(val);
            Console.WriteLine(val);
        }
    }

    public class ChainIO : IO
    {
        public static int lastOutput;

        public Queue<int> inputs = new Queue<int>();
        public ChainIO next;

        public int input()
        {
            return inputs.Count == 0 ? 0 : inputs.Dequeue();
        }

        public void output(int val)
        {
            lastOutput = val;
            next.inputs.Enqueue(val);
        }
    }

    public class PhaseIO : IO
    {
        public int[] phases;
        public Queue<int> outputs = new Queue<int>();
        public int inCount;

        public void Reset()
        {
            outputs.Clear();
            inCount = 0;
        }

        public void output(int val)
        {
            outputs.Enqueue(val);
        }

        public int input()
        {
            if (inCount % 2 == 0)
            {
                return phases[inCount++ / 2];
            }
            inCount++;
            return outputs.Count == 0 ? 0 : outputs.Dequeue();
        }
    }

}
