namespace AoC2016
{
    internal abstract class BaseDay2016 : BaseDay
    {
        public BaseDay2016()
        {
            Task.Run(() => Initialize(2016)).Wait();
        }
    }
}
