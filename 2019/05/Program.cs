using System;
using System.Linq;
using AoC.VM;

namespace AoC._2019._05
{
    class Program
    {
        static void Main()
        {
            #region Template

            var lines = AoCUtil.GetAocInput(2019, 5).ToArray<string>();
            var input = from L in lines[0].Split(",") select (int.Parse(L));

            #endregion Template

            #region Problem

            VM_2019 vm = new VM_2019(input.ToArray<int>());
            vm.io = new ConstInputIO(0);

            vm.ops[3] = (mode, vm) =>
            {
                vm.mem[vm.mem[vm.p + 1]] = vm.io.input();
                return vm.p + 2;
            };
            vm.ops[4] = (mode, vm) =>
            {
                vm.io.output(mode[2] == 0 ? vm.mem[vm.mem[vm.p + 1]] : vm.mem[vm.p + 1]);
                return vm.p + 2;
            };

            Console.Write("Part 1: ");
            vm.Go();

            vm.ops[5] = (mode, vm) =>
            {
                var v1 = VM_2019.Param(mode, 1, vm);
                var v2 = VM_2019.Param(mode, 2, vm);
                return v1 == 0 ? vm.p + 3 : v2;
            };
            vm.ops[6] = (mode, vm) =>
            {
                var v1 = VM_2019.Param(mode, 1, vm);
                var v2 = VM_2019.Param(mode, 2, vm);
                return v1 == 0 ? v2 : vm.p + 3;
            };
            vm.ops[7] = (mode, vm) =>
            {
                var v1 = VM_2019.Param(mode, 1, vm);
                var v2 = VM_2019.Param(mode, 2, vm);
                vm.mem[vm.mem[vm.p + 3]] = (v1 < v2) ? 1 : 0;
                return vm.p + 4;
            };
            vm.ops[8] = (mode, vm) =>
            {
                var v1 = VM_2019.Param(mode, 1, vm);
                var v2 = VM_2019.Param(mode, 2, vm);
                vm.mem[vm.mem[vm.p + 3]] = (v1 == v2) ? 1 : 0;
                return vm.p + 4;
            };

            Console.Write("Part 2: ");
            ((ConstInputIO)vm.io).inVal = 5;
            vm.Reset();
            vm.Go();

            #endregion
        }
    }
}
