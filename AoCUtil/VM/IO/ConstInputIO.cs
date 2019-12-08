using System;
using System.Collections.Generic;

namespace AoC.VM
{
    public class ConstInputIO : IOContext
    {
        public int InVal;
        public Queue<int> Outputs = new Queue<int>();

        public ConstInputIO(int val)
        {
            InVal = val;
        }

        public int Input()
        {
            return InVal;
        }

        public void Output(int val)
        {
            Outputs.Enqueue(val);
            Console.WriteLine(val);
        }
    }
}
