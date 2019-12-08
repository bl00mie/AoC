using System;
namespace AoC.VM.IntCode
{
    public class Op
    {
        public OpCode OpCode { get; }
        public int Argc { get;  }
        public Action<int,int,int> Action { get; }
        public int? RefI { get; }

        public Op(OpCode opCode, int argc, int? refI, Action<int,int,int> action)
        {
            OpCode = opCode;
            Argc = argc;
            RefI = refI;
            Action = action;
        }
    }
}
