using System;
using System.Linq;
using AoC.VM;
using AoC.VM.IntCode;

namespace AoC._2019._05
{
    class Program
    {
        static void Main()
        {
            #region Template

            var input = AoCUtil.GetAocInput(2019, 5).First().GetInts();

            #endregion Template

            #region Problem

            VM_2019<ConstInputIO> vm = new VM_2019<ConstInputIO>(input.ToArray<int>());
            vm.IO = new ConstInputIO(1);

            Console.Write("Part 1: ");
            vm.Go();

            Console.Write("Part 2: ");
            vm.IO.InVal = 5;
            vm.Reset();
            vm.Go();

            #endregion
        }
    }
}
