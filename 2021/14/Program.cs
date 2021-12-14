using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2021._14
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            var input = AoCUtil.GroupInput(2021, 14) as List<IEnumerable<string>>;
            var template = input[0].First();
            var rules = input[1]
                .Extract<(string pair, string insert)>(@"(\w+) -> (\w+)")
                .ToDictionary(tuple => tuple.pair, tuple => tuple.insert);
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            template = input[0].First();
            var pairs = new AoCDictionary<string, long>();
            for (int p = 0; p < template.Length-1; p++)
                pairs[$"{template[p]}{template[p + 1]}"]++;
            for (int i=0; i<40; i++)
            {
                var next = new AoCDictionary<string, long>();
                foreach (var pair in pairs.Keys)
                {
                    next[$"{pair[0]}{rules[pair]}"] += pairs[pair];
                    next[$"{rules[pair]}{pair[1]}"] += pairs[pair];
                }
                pairs = next;
                if (i == 9 || i == 39)
                {
                    var counts = pairs.GroupBy(pair => pair.Key[0]).ToDictionary(g => g.Key, g => g.Sum(e => e.Value));
                    counts[input[0].First()[^1]]++;
                    Ans(counts.Max(e => e.Value) - counts.Min(e => e.Value), i==9 ? 1 : 2);
                }
            }
        }
    }
}

