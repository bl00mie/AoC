﻿namespace AoC2023
{
    internal class Day04 : BaseDay2023
    {
        List<(int cn, HashSet<int> w, HashSet<int> m)> input = [];
        public override void ProcessInput()
        {
            input = Input
                .Extract<(int cn, string ws, string ms)>(@"Card ([0-9 ]+)\: ([0-9 ]+)\|([0-9 ]+)")
                .Select(x =>
                {
                    var w = x.ws.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToHashSet<int>();
                    var m = x.ms.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToHashSet<int>();
                    return (x.cn, w, m);
                })
                .ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            var ans = 0;
            var matches = new Dictionary<int, int>();
            var copies = new AoCDictionary<int, int>();
            foreach (var (cn, w, m) in input)
            {
                var match = w.Intersect(m).Count();
                if (match > 0)
                    ans += (int)Math.Pow(2, match - 1);
                matches[cn] = match;
                for (int i = 1; i <= match; i++)
                    copies[cn + i]++;
            }

            return Answer(ans);
        }

        public override ValueTask<string> Solve_2()
        {
            var matches = new Dictionary<int, int>();
            var copies = new AoCDictionary<int, int>();
            foreach (var (cn, w, m) in input)
            {
                var match = w.Intersect(m).Count();
                matches[cn] = match;
                for (int i = 1; i <= match; i++)
                    copies[cn + i]++;
            }

            var ans2 = input.Count;
            while (copies.Count > 0)
            {
                ans2 += copies.Sum(x => x.Value);
                var next = new AoCDictionary<int, int>();
                foreach (var x in copies)
                {
                    var num = x.Value;
                    var cn = x.Key;
                    var match = matches[cn];
                    for (int i = 1; i <= match; i++)
                        next[cn + i] += num;
                }
                copies = next;
            }
            return Answer(ans2);
        }
    }
}
