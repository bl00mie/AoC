using System.Linq;
using System.Collections.Generic;
using RegExtract;

namespace AoCUtil.VM.VM_2020
{
    public class VM_2020
    {
        public List<(string instr, int a)> code = new();
        public int IP = 0;
        public long ACC = 0;

        Dictionary<string, Op> Ops;

        public VM_2020(IEnumerable<string> input)
        {
            Ops = new[]
            {
                new Op("nop", 1, (ref int a, ref int b, ref int c) => { return 1; }),
                new Op("acc", 1, (ref int a, ref int b, ref int c) => { ACC += a; return 1; }),
                new Op("jmp", 1, (ref int a, ref int b, ref int c) => { return a; }),

            }.ToDictionary(op => op.instruction);

            code = input.Select(x => x.Extract<(string instr, int code)>(@"^(.+) ([+-]\d+)$")).ToList();
        }

        public void Reset()
        {
            IP = 0;
            ACC = 0;
        }
            
        public bool Run(bool breakOnLoop)
        {
            HashSet<long> seen = new();
            while (true)
            {
                if (IP == code.Count)
                    return true;
                if (breakOnLoop)
                {
                    if (seen.Contains(IP)) return false;
                    seen.Add(IP);
                }
                (string instr, int val) = code[IP];
                //IP += Ops[instr].F(ref val);
            }
        }
    }
}
