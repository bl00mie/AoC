namespace AoCUtil.VM.VM_2020
{
    public delegate int OpF(ref int a, ref int b, ref int c);
    public class Op
    {
        public string instruction{ get; }
        public int argc;
        public OpF F { get; }

        public Op(string instruction, int argc, OpF f)
        {
            this.instruction = instruction;
            this.argc = argc;
            F = f;
        }
    }
}
