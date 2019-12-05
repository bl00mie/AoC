using System;
using System.Collections.Generic;

namespace AoC
{
    public class VM_2019
    {
        public int p { get; set; } = 0;
        public int[] mem { get; set; }
        public Func<int> inF;
        public Func<int, bool> outF;

        public static int param(int[] mem, int p, int[] mode, int pos)
        {
            return mem[mode[mode.Length - pos] == 0 ? mem[p + pos] : p + pos];
        }


        public Dictionary<int, Func<VM_2019, int[], int>> ops { get; set; } = new Dictionary<int, Func<VM_2019, int[], int>>()
        {
            { // add
                1, (vm, mode) => {
                    var v1 = param(vm.mem, vm.p, mode, 1);
                    var v2 = param(vm.mem, vm.p, mode, 2);
                    vm.mem[vm.mem[vm.p+3]] = v1 + v2;
                    return vm.p+4;
                }
            },
            { // multiply
                2, (vm, mode) => {
                    var v1 = param(vm.mem, vm.p, mode, 1);
                    var v2 = param(vm.mem, vm.p, mode, 2);
                    vm.mem[vm.mem[vm.p+3]] = v1 * v2;
                    return vm.p+4;
                }
            },
            { // store input
                3, (vm, mode) => {
                    vm.mem[vm.mem[vm.p + 1]] = vm.inF();
                    return vm.p+2;
                }
            },
            { // write output
                4, (vm, mode) => {
                    vm.outF(mode[0] == 0 ? vm.mem[vm.mem[vm.p+1]] : vm.mem[vm.p+1]);
                    return vm.p+2;
                }
            },
            { // jump if not zero
                5, (vm, mode) => {
                    var v1 = param(vm.mem, vm.p, mode, 1);
                    var v2 = param(vm.mem, vm.p, mode, 2);
                    return v1 == 0 ? vm.p + 3 : v2;
                }
            },
            { // jump if zero
                6, (vm, mode) => {
                    var v1 = param(vm.mem, vm.p, mode, 1);
                    var v2 = param(vm.mem, vm.p, mode, 2);
                    return v1 == 0 ? v2 : vm.p + 3;
                }
            },
            { // less than
                7, (vm, mode) => {
                    var v1 = param(vm.mem, vm.p, mode, 1);
                    var v2 = param(vm.mem, vm.p, mode, 2);
                    vm.mem[vm.mem[vm.p+3]] = (v1 < v2) ? 1 : 0;
                    return vm.p+4;
                }
            },
            { // equal to
                8, (vm, mode) => {
                    var v1 = param(vm.mem, vm.p, mode, 1);
                    var v2 = param(vm.mem, vm.p, mode, 2);
                    vm.mem[vm.mem[vm.p+3]] = (v1 == v2) ? 1 : 0;
                    return vm.p+4;
                }
            }
        };

        public VM_2019() { }

        public VM_2019(int[] memory)
        {
            mem = memory;
        }

        public void go()
        {
            while (mem[p] != 99)
            {
                var op = mem[p] % 100;
                var modeI = mem[p] / 100;
                var mode = new int[] { modeI / 100, modeI % 100 / 10, modeI % 10 };
                p = ops[op](this, mode);
            }
        }
    }
}
