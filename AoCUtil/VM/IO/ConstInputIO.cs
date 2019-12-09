using System;
using System.Collections.Generic;

namespace AoC.VM
{
    public class ConstInputIO : IOContext
    {
        public long InVal;
        public Queue<long> Outputs = new Queue<long>();
        public bool Debug;

        public ConstInputIO(int val)
        {
            InVal = val;
        }

        public long Input()
        {
            return InVal;
        }

        public void Output(long val)
        {
            Outputs.Enqueue(val);
            if (Debug) Console.WriteLine(val);
        }

        public void Reset()
        {
            Outputs.Clear();
        }
    }
}
