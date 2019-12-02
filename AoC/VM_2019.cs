using System;
using System.Collections.Generic;

namespace AoC
{
    public class VM_2019
    {
        public int p { get; set; }
        public int[] mem { get; set; }

        public Dictionary<int, Func<int[], int, int>> ops { get; set; } = new Dictionary<int, Func<int[], int, int>>()
        {
            { 1, (mem, p) => { mem[mem[p + 3]] = mem[mem[p + 1]] + mem[mem[p + 2]]; return p+4; } },
            { 2, (mem, p) => { mem[mem[p + 3]] = mem[mem[p + 1]] * mem[mem[p + 2]]; return p+4; } }
        };

        public VM_2019() { }

        public VM_2019(int[] memory)
        {
            mem = memory;
            p = 0;
        }

        public void go()
        {
            while (mem[p] != 99)
            {
                p = ops[mem[p]](mem, p);
            }
        }
    }
}
