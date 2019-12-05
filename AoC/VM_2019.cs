using System;
using System.Collections.Generic;

namespace AoC
{
    public class VM_2019
    {
        public static int p { get; set; } = 0;
        public static int[] mem { get; set; }
        private static int[] initialMem { get; set; }
        public static Func<int> inF;
        public static Action<int> outF;

        public Dictionary<int, Func<int[], int>> ops { get; set; } = new Dictionary<int, Func<int[], int>>()
        {
            { // add
                1, (mode) => {
                    var v1 = Param(mode, 1);
                    var v2 = Param(mode, 2);
                    mem[mem[p+3]] = v1 + v2;
                    return p+4;
                }
            },
            { // multiply
                2, (mode) => {
                    var v1 = Param(mode, 1);
                    var v2 = Param(mode, 2);
                    mem[mem[p+3]] = v1 * v2;
                    return p+4;
                }
            },
            { // store input
                3, (mode) => {
                    mem[mem[p + 1]] = inF();
                    return p+2;
                }
            },
            { // write output
                4, (mode) => {
                    outF(Param(mode, 1));
                    return p+2;
                }
            },
            { // jump if not zero
                5, (mode) => {
                    var v1 = Param(mode, 1);
                    var v2 = Param(mode, 2);
                    return v1 == 0 ? p + 3 : v2;
                }
            },
            { // jump if zero
                6, (mode) => {
                    var v1 = Param(mode, 1);
                    var v2 = Param(mode, 2);
                    return v1 == 0 ? v2 : p + 3;
                }
            },
            { // less than
                7, (mode) => {
                    var v1 = Param(mode, 1);
                    var v2 = Param(mode, 2);
                    mem[mem[p+3]] = (v1 < v2) ? 1 : 0;
                    return p+4;
                }
            },
            { // equal to
                8, (mode) => {
                    var v1 = Param(mode, 1);
                    var v2 = Param(mode, 2);
                    mem[mem[p+3]] = (v1 == v2) ? 1 : 0;
                    return p+4;
                }
            }
        };

        public VM_2019() { }

        public VM_2019(int[] memory)
        {
            initialMem = (int[])memory.Clone();
            Reset();
        }

        public static int Param(int[] mode, int pos)
        {
            return mem[mode[mode.Length - pos] == 0 ? mem[p + pos] : p + pos];
        }

        public void Reset()
        {
            p = 0;
            mem = (int[])initialMem.Clone();
        }


        public void Go()
        {
            while (mem[p] != 99)
            {
                var op = mem[p] % 100;
                var modeI = mem[p] / 100;
                var mode = new int[] { modeI / 100, modeI % 100 / 10, modeI % 10 };
                p = ops[op](mode);
            }
        }
    }
}
