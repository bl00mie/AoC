using System;
using System.Collections.Generic;

namespace AoC.VM
{
    public class ChainIO : IOContext
    {
        public static long lastOutput;

        public Queue<long> Inputs = new Queue<long>();
        public ChainIO Next;
        public int Id;
        public bool Debug;

        public long Input()
        {
            return Inputs.Count == 0 ? 0 : Inputs.Dequeue();
        }

        public void Output(long val)
        {
            lastOutput = val;
            if (Debug) Console.WriteLine(string.Format("[{0}]: {1} -> {2}", Id, val, Next.Id));
            Next.Inputs.Enqueue(val);
        }

        public void Reset()
        {
            if (Debug) Console.WriteLine(string.Format("Resetting IO {0}", Id));
            Inputs.Clear();
            lastOutput = 0;
        }
    }
}
