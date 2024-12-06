using AoC.VM;
using AoC.VM.IntCode;

namespace AoC2019.Solutions
{
    public class Day02() : BaseDay(2019)
    {
        long[] Memory = [];
        public override void ProcessInput()
        {
            Memory = Input[0].Split(",").Select(long.Parse).ToArray();
        }

        public override dynamic Solve_1()
        {
            var vm = new VM_2019<IOContext>(Memory);
            vm.Mem[1] = 12;
            vm.Mem[2] = 2;
            vm.Go();
            return vm.Mem[0];
        }

        public override dynamic Solve_2()
        {
            var vm = new VM_2019<IOContext>([.. Memory]);
            for (int i = 99; i >= 0; i--)
                for (int j = 0; j < 100; j++)
                {
                    vm.Mem = [.. Memory];
                    vm.Mem[1] = i;
                    vm.Mem[2] = j;
                    vm.P = 0;
                    vm.Go();

                    if (vm.Mem[0] == 19690720)
                        return i * 100 + j;
                }
            throw new Exception("fail");
        }
    }
}
