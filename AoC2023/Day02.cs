﻿using System.Collections.Immutable;
using System.Diagnostics;

namespace AoC2023
{
    internal class Day02 : BaseDay2023
    {

        List<(int id, string game)> Games = [];
        readonly ImmutableDictionary<string, int> _max = new Dictionary<string, int> { ["red"] = 12, ["green"] = 13, ["blue"] = 14 }.ToImmutableDictionary();

        public override void ProcessInput()
        {
            Games = Input.Extract<(int, string)>(@"Game (\w+): ([a-z0-9;, ]+)").ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            var ans = 0;
            foreach (var (id, game) in Games)
            {
                var draws = game.Split("; ");
                var possible = true;
                foreach (var draw in draws)
                    foreach (var (v, color) in draw.Split(", ").Extract<(int, string)>(@"(\w+) (\w+)"))
                        if (v > _max[color]) possible = false;
                if (possible) ans += id;
            }

            return Answer(ans);
        }

        public override ValueTask<string> Solve_2()
        {
            var ans = 0;
            foreach (var (id, game) in Games)
            {
                var draws = game.Split("; ");
                var min = new AoCDictionary<string, int>(0, true);
                foreach (var draw in draws)
                    foreach (var (v, color) in draw.Split(", ").Extract<(int, string)>(@"(\w+) (\w+)"))
                        if (v > min[color]) min[color] = v;
                ans += min["red"] * min["green"] * min["blue"];
            }
            return Answer(ans);
        }
    }
}
