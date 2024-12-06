using AoC.VM.IntCode;
using AoC.VM;

namespace AoC2019.Solutions
{
    public class Day09() : BaseDay(2019)
    {
        long[] Memory = [];
        public override void ProcessInput()
        {
            Memory = Input[0].Split(",").Select(long.Parse).ToArray();
        }

        public override dynamic Solve_1()
        {
            VM_2019<ConstInputIO> vm = new(Memory)
            {
                IO = new ConstInputIO(1)
            };
            vm.Go();
            return vm.IO.Outputs.Peek();
        }

        public override dynamic Solve_2()
        {
            VM_2019<ConstInputIO> vm = new(Memory)
            {
                IO = new ConstInputIO(2)
            };
            vm.Go();
            return vm.IO.Outputs.Peek();
        }
    }
}
