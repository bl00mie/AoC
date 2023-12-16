namespace AoC2023.Solutions
{
    internal class Day07 : BaseDay2023
    {
        (string hand, int bid)[] hands = [];
        public override void ProcessInput()
        {
            hands = Input.Extract<(string hand, int bid)>(@"(\w+) (\d+)").ToArray();
        }

        static string Score(string hand, int part)
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

        public override dynamic Solve_1()
        {
            return hands.Select(x => (Score(x.hand, 1), x.bid))
                .OrderBy(x => x.Item1)
                .Select((x, i) => x.bid * (i + 1))
                .Sum();
        }

        public override dynamic Solve_2()
        {
            return hands.Select(x => (Score(x.hand, 2), x.bid))
                .OrderBy(x => x.Item1)
                .Select((x, i) => x.bid * (i + 1))
                .Sum();
        }
    }
}
