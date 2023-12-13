namespace AoC2023
{
    internal abstract class BaseDay2023 : BaseDay
    {
        public BaseDay2023()
        {
            Task.Run(() => Initialize(2023)).Wait();
        }
    }
}
