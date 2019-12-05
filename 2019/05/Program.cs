using System;
using System.Linq;

namespace AoC._2019._05
{
    class Program
    {
        static void Main()
        {
            #region Template

            var lines = AoCUtil.getAocInput(2019, 5).ToArray<string>();

            var norms = from L in lines[0].Split(",") select (int.Parse(L));

            #endregion Template

            #region Problem

            VM_2019 vm = new VM_2019(norms.ToArray<int>());

            vm.ops[3] = (vm, mode) => {
                vm.mem[vm.mem[vm.p + 1]] = vm.inF();
                return vm.p + 2;
            };
            vm.ops[4] = (vm, mode) => {
                vm.outF(mode[0] == 0 ? vm.mem[vm.mem[vm.p + 1]] : vm.mem[vm.p + 1]);
                return vm.p + 2;
            };

            vm.outF = (v) => { Console.WriteLine(v); return false; };
            Console.Write("Part 1: ");
            vm.inF = () => { return 1; };
            vm.go();


            vm.ops[5] = (vm, mode) => {
                var v1 = VM_2019.param(vm.mem, vm.p, mode, 1);
                var v2 = VM_2019.param(vm.mem, vm.p, mode, 2);
                return v1 == 0 ? vm.p + 3 : v2;
            };
            vm.ops[6] = (vm, mode) => {
                var v1 = VM_2019.param(vm.mem, vm.p, mode, 1);
                var v2 = VM_2019.param(vm.mem, vm.p, mode, 2);
                return v1 == 0 ? v2 : vm.p + 3;
            };
            vm.ops[7] = (vm, mode) => {
                var v1 = VM_2019.param(vm.mem, vm.p, mode, 1);
                var v2 = VM_2019.param(vm.mem, vm.p, mode, 2);
                vm.mem[vm.mem[vm.p + 3]] = (v1 < v2) ? 1 : 0;
                return vm.p + 4;
            };
            vm.ops[8] = (vm, mode) => {
                var v1 = VM_2019.param(vm.mem, vm.p, mode, 1);
                var v2 = VM_2019.param(vm.mem, vm.p, mode, 2);
                vm.mem[vm.mem[vm.p + 3]] = (v1 == v2) ? 1 : 0;
                return vm.p + 4;
            };

            Console.Write("Part 2: ");
            vm.inF = () => { return 5; };
            vm.mem = norms.ToArray<int>();
            vm.p = 0;
            vm.go();

            #endregion
        }
    }
}
