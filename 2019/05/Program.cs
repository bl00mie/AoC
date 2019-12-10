using System.Linq;
using AoC.VM;
using AoC.VM.IntCode;

namespace AoC._2019._05
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region Template

            var input = AoCUtil.GetAocInput(2019, 5).First().GetLongs();

            #endregion Template

            #region Problem

            VM_2019<ConstInputIO> vm = new VM_2019<ConstInputIO>(input.ToArray());
            vm.IO = new ConstInputIO(1);

            vm.Go();
            Ans(vm.IO.Outputs.Last());

            vm.IO.InVal = 5;
            vm.Reset();
            vm.IO.Reset();
            vm.Go();
            Ans(vm.IO.Outputs.Last());

            #endregion
        }
    }
}
