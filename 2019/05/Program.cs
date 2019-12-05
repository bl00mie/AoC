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
            vm.inF = () => { return 5; };
            vm.outF = (v) => { Console.WriteLine(v); return false; };
            vm.go();

            #endregion
        }
    }
}
