using System.Collections.Generic;
using AoCUtil.VM.VM_2020;

namespace AoC._2020._08
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2020, 08);
            #endregion

            #region Part 1
            VM_2020 vm = new VM_2020(input);
            vm.Run(true);
            Ans(vm.ACC);
            #endregion Part 1

            #region Part 2
            vm.Reset();
            for (int i = 0; i < vm.code.Count; i++)
            {
                if (vm.code[i].instr == "acc") continue;
                swap(vm.code, i);
                if (vm.Run(true))
                    break;
                swap(vm.code, i);
                vm.Reset();
            }
            Ans2(vm.ACC);
            #endregion
        }
        static void swap(List<(string, int)> code, int i)
        {
            (string ins, int a) = code[i];
            if (ins == "jmp")
                code[i] = ("nop", a);
            else 
                code[i] = ("jmp", a);
        }
    }
}

