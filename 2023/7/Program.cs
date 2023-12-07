using System.Linq;
using RegExtract;

namespace AoC._2023._7
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 7).Extract<(string hand, int bid)>(@"(\w+) (\d+)").ToArray();

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            foreach (var part in Enumerable.Range(1, 2))
                Ans(input.Select(p => (Score(p.hand, part), p.bid))
                    .OrderBy(a => a.Item1)
                    .Select((x, i) => x.bid * (i + 1)).Sum(),
                    part);
        }

        private static string Score(string hand, int part)
        {
            var cards = hand.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            if (part == 2)
                if (cards.Remove('J', out var jokers))
                    if (cards.Count != 0)
                        cards[cards.MaxBy(p => p.Value).Key] += jokers;
                    else // handle "JJJJJ"
                        cards['J'] = 5;
            var score = $"{cards.Count switch
            {
                1 => 7,
                2 => cards.ContainsValue(4) ? 6 : 5,
                3 => cards.ContainsValue(3) ? 4 : 3,
                4 => 2,
                _ => 1
            }}";
            score += string.Join("", hand.Select(c => c switch
            {
                'T' => '9' + 1,
                'J' => part == 1 ? '9' + 2 : '1',
                'Q' => '9' + 3,
                'K' => '9' + 4,
                'A' => '9' + 5,
                _ => c
            }));
            return score;
        }
    }
}
