using System;
using System.Linq;

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

            vm.ops[3] = (mode) =>
            {
                VM_2019.mem[VM_2019.mem[VM_2019.p + 1]] = VM_2019.inF();
                return VM_2019.p + 2;
            };
            vm.ops[4] = (mode) =>
            {
                VM_2019.outF(mode[2] == 0 ? VM_2019.mem[VM_2019.mem[VM_2019.p + 1]] : VM_2019.mem[VM_2019.p + 1]);
                return VM_2019.p + 2;
            };

            VM_2019.outF = Console.WriteLine;
            VM_2019.inF = () => { return 1; };

            Console.Write("Part 1: ");
            vm.Go();

            vm.ops[5] = (mode) =>
            {
                var v1 = VM_2019.Param(mode, 1);
                var v2 = VM_2019.Param(mode, 2);
                return v1 == 0 ? VM_2019.p + 3 : v2;
            };
            vm.ops[6] = (mode) =>
            {
                var v1 = VM_2019.Param(mode, 1);
                var v2 = VM_2019.Param(mode, 2);
                return v1 == 0 ? v2 : VM_2019.p + 3;
            };
            vm.ops[7] = (mode) =>
            {
                var v1 = VM_2019.Param(mode, 1);
                var v2 = VM_2019.Param(mode, 2);
                VM_2019.mem[VM_2019.mem[VM_2019.p + 3]] = (v1 < v2) ? 1 : 0;
                return VM_2019.p + 4;
            };
            vm.ops[8] = (mode) =>
            {
                var v1 = VM_2019.Param(mode, 1);
                var v2 = VM_2019.Param(mode, 2);
                VM_2019.mem[VM_2019.mem[VM_2019.p + 3]] = (v1 == v2) ? 1 : 0;
                return VM_2019.p + 4;
            };

            Console.Write("Part 2: ");
            VM_2019.inF = () => { return 5; };
            vm.Reset();
            vm.Go();

            #endregion
        }
    }
}
