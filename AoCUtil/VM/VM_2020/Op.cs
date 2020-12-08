namespace AoCUtil.VM.VM_2020
{
    public delegate int OpF(ref int a);
    public class Op
    {
        public string instruction{ get; }
        public OpF F { get; }

        public Op(string instruction, OpF f)
        {
            this.instruction = instruction;
            F = f;
        }
    }
}
