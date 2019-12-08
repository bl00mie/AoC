using System.Collections.Generic;

namespace AoC.VM
{
    public class ChainIO : IOContext
    {
        public static int lastOutput;

        public Queue<int> inputs = new Queue<int>();
        public ChainIO next;

        public int Input()
        {
            return inputs.Count == 0 ? 0 : inputs.Dequeue();
        }

        public void Output(int val)
        {
            lastOutput = val;
            next.inputs.Enqueue(val);
        }
    }
}
