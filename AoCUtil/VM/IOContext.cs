using System;
namespace AoC.VM
{
    public interface IOContext
    {
        void output(int val);
        int input();
    }
}
