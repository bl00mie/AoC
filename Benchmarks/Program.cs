using BenchmarkDotNet.Running;

namespace AoC.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchmarkPermuters>();
        }
    }
}
