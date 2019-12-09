namespace AoC.VM.IntCode
{
    public delegate int OpF(ref long a, ref long b, ref long c);
    public class Op
    {
        public OpCode OpCode { get; }
        public int Argc { get;  }
        public OpF F { get; }

        public Op(OpCode opCode, int argc, OpF f)
        {
            OpCode = opCode;
            Argc = argc;
            F = f;
        }
    }
}
