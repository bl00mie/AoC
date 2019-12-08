﻿using System.Collections.Generic;
using System.Linq;
using AoC.VM;
using AoC.VM.IntCode;

namespace AoC._2019._07
{
    public sealed class Program : ProgramBase
    {
        static void Main()
        {
            #region input

            var input = AoCUtil.GetAocInput(2019, 07).First().GetInts().ToArray();
            var perms = AoCUtil.GetPermutations<int>(Enumerable.Range(0,5));

            #endregion input

            #region Part 1

            VM_2019<PhaseIO> vm = new VM_2019<PhaseIO>(input);
            vm.IO = new PhaseIO();
            int max = int.MinValue;
            foreach (var perm in perms)
            {
                vm.IO.Reset();
                vm.IO.phases = perm.ToArray();
                for(int i=0; i<5; i++) 
                {
                    vm.Reset();
                    vm.Go();
                }
                var output = vm.IO.outputs.Dequeue();
                if (output > max) max = output;
            }
            Ans(max.ToString());

            #endregion Part 1

            #region Part 2

            perms = AoCUtil.GetPermutations<int>(Enumerable.Range(5, 5));
            VM_2019<ChainIO>[] vms = new VM_2019<ChainIO>[5];
            for (int i=4; i>=0; i--)
            {
                vms[i] = new VM_2019<ChainIO>(input, true);
                vms[i].IO = new ChainIO();
                if (vms[(i+1)%5] != null)
                {
                    vms[i].IO.next = vms[(i + 1) % 5].IO;
                }
            }
            vms[4].IO.next = vms[0].IO;

            max = int.MinValue;
            foreach (var perm in perms)
            {
                var pa = perm.ToArray();
                for (int i=0; i<5; i++)
                {
                    vms[i].IO.inputs.Clear();
                    vms[i].IO.inputs.Enqueue(pa[i]);
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
            Ans(max, 2);

            #endregion Part 2
        }
    }

    public class PhaseIO : IOContext
    {
        public int[] phases;
        public Queue<int> outputs = new Queue<int>();
        public int inCount;

        public void Reset()
        {
            outputs.Clear();
            inCount = 0;
        }

        public void Output(int val)
        {
            outputs.Enqueue(val);
        }

        public int Input()
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

