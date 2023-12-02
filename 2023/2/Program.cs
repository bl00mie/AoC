using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RegExtract;

namespace AoC._2023._2
{
    class Program : ProgramBase
    {
        private static readonly ImmutableDictionary<string, int> _max = new Dictionary<string, int> { ["red"] = 12, ["green"] = 13, ["blue"] = 14 }.ToImmutableDictionary();
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 2).Extract<(int, string)>(@"Game (\w+): ([a-z0-9;, ]+)").ToList();

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            var ans = 0;
            var ans2 = 0L;
            foreach (var (id, game) in input)
            {
                var draws = game.Split("; ");
                var possible = true;
                var min = new AoCDictionary<string, int>(0, true);
                foreach (var draw in draws)
                    foreach (var (v, color) in draw.Split(", ").Extract<(int, string)>(@"(\w+) (\w+)"))
                    {
                        if (v > _max[color]) possible = false;
                        if (v > min[color]) min[color] = v;
                    }
                if (possible) ans += id;
                ans2 += min.Values.Aggregate((x, y) => x * y);
            }

            Ans(ans);
            Ans2(ans2);
        }
    }
}

