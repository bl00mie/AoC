using System.Linq;
using AoC.VM;

namespace AoC._2019._07
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input

            var input = AoCUtil.GetAocInput(2019, 07).First().GetInts().ToArray();
            var perms = AoCUtil.GetPermutations<int>(Enumerable.Range(0,5));

            #endregion input

            #region Part 1

            VM_2019 vm = new VM_2019(input);
            PhaseIO io = new PhaseIO();
            vm.io = io;
            int max = int.MinValue;
            foreach (var perm in perms)
            {
                io.Reset();
                io.phases = perm.ToArray();
                for(int i=0; i<5; i++) 
                {
                    vm.Reset();
                    vm.Go();
                }
                var output = io.outputs.Dequeue();
                if (output > max) max = output;
            }
            Log(max.ToString());

            #endregion Part 1

            #region Part 2

            perms = AoCUtil.GetPermutations<int>(Enumerable.Range(5, 5));
            VM_2019[] vms = new VM_2019[5];
            for (int i=4; i>=0; i--)
            {
                vms[i] = new VM_2019(input, true);
                ChainIO cio = new ChainIO();
                vms[i].io = cio;
                if (vms[(i+1)%5] != null)
                {
                    cio.next = (ChainIO)vms[(i + 1) % 5].io;
                }
            }
            ((ChainIO)vms[4].io).next = (ChainIO)vms[0].io;

            max = int.MinValue;
            foreach (var perm in perms)
            {
                var pa = perm.ToArray();
                for (int i=0; i<5; i++)
                {
                    ((ChainIO)vms[i].io).inputs.Clear();
                    ((ChainIO)vms[i].io).inputs.Enqueue(pa[i]);
                    vms[i].Reset();
                }
                for (int i = 0; ; i = (i+1)%5)
                {
                    if (vms[i].Go() < 0)
                    {
                        if (ChainIO.lastOutput > max) max = ChainIO.lastOutput;
                        break;
                    }
                }
            }
            Log(max);

            #endregion Part 2
        }
    }
}

