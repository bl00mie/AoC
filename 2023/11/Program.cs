using System.Linq;
using AoCUtil;

namespace AoC._2023._11
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 11).ToArray();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            var A = new[] { 1, 2, 3, 4 };
            var B = A.Skip(4).ToList();

            foreach (var part in Enumerable.Range(1,2))
            {
                var ER = Enumerable.Range(0, input.Length)
                    .Where(i => input[i].All(c => c == '.'))
                    .ToHashSet();
                var EC = Enumerable.Range(0, input[0].Length)
                    .Where(i => input.All(l => l[i] == '.'))
                    .ToHashSet();
                var G = input.SelectMany((l, y) => 
                    l.Select((c, x) => (x, y, c))
                    .Where(t => t.c == '#')
                    .Select(t => (t.x, t.y)))
                    .ToList();

                var delta = part == 1 ? 1 : 999_999;
                var ans = G.ForAllPairs<(int x, int y), long>((a, b) =>
                {
                    var d = 0;
                    for (int x = a.x; x != b.x; x += a.x < b.x ? 1 : -1)
                        d += 1 + (EC.Contains(x) ? delta : 0);
                    for (int y = a.y; y != b.y; y += a.y < b.y ? 1 : -1)
                        d += 1 + (ER.Contains(y) ? delta : 0);
                    return d;
                }).ToArray().Sum();

                Ans(ans, part);
            }
        }
    }
}

