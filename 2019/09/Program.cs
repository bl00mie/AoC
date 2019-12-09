using System.Linq;
using AoC.VM;
using AoC.VM.IntCode;

namespace AoC._2019._09
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            
            var input = AoCUtil.GetAocInput(2019, 09).First().GetLongs().ToArray();

            #endregion

            #region Part 1

            VM_2019<ConstInputIO> vm = new VM_2019<ConstInputIO>(input)
            {
                IO = new ConstInputIO(1)
            };
            vm.Go();
            Ans(vm.IO.Outputs.Peek());

            #endregion Part 1

            #region Part 2

            vm.Reset();
            vm.IO.InVal = 2;
            vm.IO.Reset();
            vm.Go();
            Ans(vm.IO.Outputs.Peek(), 2);
            #endregion
        }
    }
}

