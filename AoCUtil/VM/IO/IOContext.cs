namespace AoC.VM
{
    public interface IOContext
    {
        void Output(long val);
        long Input();
        void Reset();
    }
}
