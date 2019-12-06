using System;
using System.Linq;

namespace AoC._2019._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = AoCUtil.GetAocInput(2019, 2);

            int[] initial = lines.Single<string>().Split(",").Select(s => int.Parse(s)).ToArray<int>();

            #region Problem Part 1

            VM_2019 vm = new VM_2019((int[])initial.Clone());
            VM_2019.mem[1] = 12;
            VM_2019.mem[2] = 2;
            vm.Go();
            Console.WriteLine(VM_2019.mem[0]);

            #endregion

            #region Problem Part 2

            //VM_2019 vm = new VM_2019();
            for (int i=0; i<100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    VM_2019.mem = (int[])initial.Clone();
                    VM_2019.mem[1] = i;
                    VM_2019.mem[2] = j;
                    VM_2019.p = 0;
                    vm.Go();

                    if (VM_2019.mem[0] == 19690720)
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
