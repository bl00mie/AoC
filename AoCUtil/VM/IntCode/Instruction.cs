using System;
namespace AoC.VM.IntCode
{
    public class Op
    {
        public OpCode opCode { get; }
        public int argc { get;  }
        public Action<int,int,int> action { get; }
        public int? refI { get; }

        public Op(OpCode opCode, int argc, int? refI, Action<int,int,int> action)
        {
            this.opCode = opCode;
            this.argc = argc;
            this.refI = refI;
            this.action = action;
        }
    }
}
