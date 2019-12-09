using System;
using System.Linq;
using AoC.VM;
using AoC.VM.IntCode;

namespace AoC._2019._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = AoCUtil.GetAocInput(2019, 2);

            long[] initial = lines.Single<string>().Split(",").Select(s => long.Parse(s)).ToArray<long>();

            #region Problem Part 1

            VM_2019<IOContext> vm = new VM_2019<IOContext>((long[])initial.Clone());
            vm.Mem[1] = 12;
            vm.Mem[2] = 2;
            vm.Go();
            Console.WriteLine(vm.Mem[0]);

            #endregion

            #region Problem Part 2

            //VM_2019 vm = new VM_2019();
            for (int i=0; i<100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    vm.Mem = (long[])initial.Clone();
                    vm.Mem[1] = i;
                    vm.Mem[2] = j;
                    vm.P = 0;
                    vm.Go();

                    if (vm.Mem[0] == 19690720)
                    {
                        Console.WriteLine(i * 100 + j);
                        i = 100; j = 100;
                    }
                }
            }

            #endregion
        }
    }
}
