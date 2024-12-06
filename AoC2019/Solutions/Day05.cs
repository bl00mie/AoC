using AoC.VM;
using AoC.VM.IntCode;

namespace AoC2019.Solutions
{
    public class Day05() : BaseDay(2019)
    {
        long[] Memory = [];
        public override void ProcessInput()
        {
            Memory = Input[0].Split(",").Select(long.Parse).ToArray();
        }

        public override dynamic Solve_1()
        {
            var vm = new VM_2019<IOContext>(Memory) { IO = new ConstInputIO(1) };
            vm.Go();
            return ((ConstInputIO)vm.IO).Outputs.Last();
        }

        public override dynamic Solve_2()
        {
            var vm = new VM_2019<IOContext>(Memory) { IO = new ConstInputIO(5) };
            vm.Go();
            return ((ConstInputIO)vm.IO).Outputs.Last();
        }
    }
}
