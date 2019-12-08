using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

namespace AoC.Benchmark
{
    [SimpleJob(RuntimeMoniker.NetCoreApp30)]
    [RPlotExporter]
    public class BenchmarkPermuters
    {
        private IEnumerable<int> data;
        private readonly Consumer consumer = new Consumer();

        [Params(5, 7, 9, 11)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            data = Enumerable.Range(0, N);
        }

        [Benchmark]
        public void Recursive() => AoCUtil.GetPermutationsRecursive<int>(data, N).Consume(consumer);

        [Benchmark]
        public void Iterative() => AoCUtil.GetPermutations<int>(data).Consume(consumer);
    }
}
