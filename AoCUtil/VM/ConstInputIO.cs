using System.Collections.Generic;

namespace AoC.VM
{
    public class ConstInputIO : IOContext
    {
        public int inVal;
        public Queue<int> outputs = new Queue<int>();

        public ConstInputIO(int val)
        {
            inVal = val;
        }

        public int input()
        {
            return inVal;
        }

        public void output(int val)
        {
            outputs.Enqueue(val);
            //Console.WriteLine(val);
        }
    }
}
