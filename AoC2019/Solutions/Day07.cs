using AoC.VM;
using AoC.VM.IntCode;

namespace AoC2019.Solutions
{
    public class Day07() : BaseDay(2019)
    {
        long[] Memory = [];
        public override void ProcessInput()
        {
            Memory = Input[0].Split(",").Select(long.Parse).ToArray();
        }

        public override dynamic Solve_1()
        {
            VM_2019<PhaseIO> vm = new(Memory)
            {
                IO = new PhaseIO()
            };
            long max = long.MinValue;
            foreach (var perm in Enumerable.Range(0, 5).GetPermutations())
            {
                vm.IO.Reset();
                vm.IO.phases = perm.ToArray();
                foreach(var _ in perm)
                {
                    vm.Reset();
                    vm.Go();
                }
                max = Math.Max(max, vm.IO.outputs.Dequeue());
            }
            return max;
        }

        public override dynamic Solve_2()
        {
            VM_2019<ChainIO>[] vms = new VM_2019<ChainIO>[5];
            for (int i = 4; i >= 0; i--)
            {
                vms[i] = new VM_2019<ChainIO>(Memory, true) { IO = new ChainIO() { Id = i + 1 } };
                if (vms[(i + 1) % 5] != null)
                    vms[i].IO.Next = vms[(i + 1) % 5].IO;
            }
            vms[4].IO.Next = vms[0].IO;

            var max = long.MinValue;
            foreach (var perm in Enumerable.Range(5, 5).GetPermutations())
            {
                var pa = perm.ToArray();
                for (int i = 0; i < 5; i++)
                {
                    vms[i].IO.Inputs.Clear();
                    vms[i].IO.Inputs.Enqueue(pa[i]);
                    vms[i].Reset();
                }
                for (int i = 0; ; i = (i + 1) % 5)
                    if (vms[i].Go() < 0)
                    {
                        max = Math.Max(ChainIO.lastOutput, max);
                        break;
                    }
            }
            return max;
        }
    }

    public class PhaseIO : IOContext
    {
        public int[] phases;
        public Queue<long> outputs = new();
        public int inCount;

        public void Reset()
        {
            outputs.Clear();
            inCount = 0;
        }

        public void Output(long val)
        {
            outputs.Enqueue(val);
        }

        public long Input()
        {
            if (inCount % 2 == 0)
            {
                return phases[inCount++ / 2];
            }
            inCount++;
            return outputs.Count == 0 ? 0 : outputs.Dequeue();
        }
    }
}
