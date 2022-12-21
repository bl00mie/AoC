using System.Linq;
using Nito.Collections;

namespace AoC._2022._20
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            var input = AoCUtil.GetAocInput(2022, 20).Select(l => int.Parse(l)).ToArray();
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var len = input.Length;
            var mixed = new Deque<(long v, int i)>(input.Select((v, i) => ((long)v, i)));
            for (int i = 0; i < len; i++)
                for (int p = 0; p < len; p++)
                    if (mixed[p].i == i)
                    {
                        var x = mixed[p];
                        mixed.RemoveAt(p);
                        mixed.Insert(mod(x.v + p, len), x);
                        //WL($"moving {x.v}:\t{string.Join(", ", mixed.Select(x => x.v))}");
                        break;
                    }

            var i0 = mixed.Where(x => x.v == 0).Single().i;
            Ans(mixed[(i0 + 1000) % len].v + mixed[(i0 + 2000) % len].v + mixed[(i0 + 3000) % len].v);
            #endregion Part 1

            #region Part 2
            mixed = new Deque<(long v, int i)>(input.Select((v, i) => (v * 811589153L, i)));
            foreach (var _ in Enumerable.Range(0, 10))
                for (int i = 0; i < len; i++)
                    for (int p = 0; p < len; p++)
                        if (mixed[p].i == i)
                        {
                            var x = mixed[p];
                            mixed.RemoveAt(p);
                            mixed.Insert(mod(x.v + p, len), x);
                            break;
                        }

            i0 = mixed.Where(x => x.v == 0).Single().i;
            Ans2(mixed[(i0 + 1000) % len].v + mixed[(i0 + 2000) % len].v + mixed[(i0 + 3000) % len].v);
            #endregion
        }

        static int mod(long x, int m) => (int)((x % m + m) % m); 
    }
}

