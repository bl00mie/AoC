namespace AoC.VM
{
    public interface IOContext
    {
        void Output(int val);
        int Input();
    }
}
