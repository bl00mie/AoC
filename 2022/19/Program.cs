using System;
using System.Linq;
using RegExtract;

namespace AoC._2022._19
{
    class Program : ProgramBase
    {
        record struct Blueprint(int id, int oo, int co, int bo, int bc, int go, int gb);
        record struct State( int o, int c, int b, int g, int or, int cr, int br, int gr, int t);
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2022, 19)
                .Extract<Blueprint>(@"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.")
                .ToArray();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1


            static int MaxGeodes(Blueprint bp, State state, int max = 0)
            {
                if (state.t == 0) return state.g;

                if (state.g + state.t * state.gr + (state.t - 1) * state.t / 2 <= max)
                    return 0;

                state.t -= 1;

                if (state.o >= bp.go && state.b >= bp.gb)
                {
                    var s = state with 
                    {
                        o = state.o + state.or - bp.go,
                        c = state.c + state.cr,
                        b = state.b + state.br - bp.gb,
                        g = state.g + state.gr,
                        gr = state.gr + 1
                    };
                    max = Math.Max(max, MaxGeodes(bp, s, max));
                }
                if (state.o >= bp.bo && state.c >= bp.bc)
                {
                    var s = state with
                    {
                        o = state.o + state.or - bp.bo,
                        c = state.c + state.cr - bp.bc,
                        b = state.b + state.br,
                        g = state.g + state.gr,
                        br = state.br + 1
                    };
                    max = Math.Max(max, MaxGeodes(bp, s, max));
                }
                if (state.o >= bp.co)
                {
                    var s = state with
                    {
                        o = state.o + state.or - bp.co,
                        c = state.c + state.cr,
                        b = state.b + state.br,
                        g = state.g + state.gr,
                        cr = state.cr + 1
                    };
                    max = Math.Max(max, MaxGeodes(bp, s, max));
                }
                if (state.o >= bp.oo && state.or < new[] { bp.co, bp.bo, bp.go }.Max())
                {
                    var s = state with
                    {
                        o = state.o + state.or - bp.oo,
                        c = state.c + state.cr,
                        b = state.b + state.br,
                        g = state.g + state.gr,
                        or = state.or + 1
                    };
                    max = Math.Max(max, MaxGeodes(bp, s, max));
                }
                state.o += state.or;
                state.c += state.cr;
                state.b += state.br;
                state.g += state.gr;

                return Math.Max(max, MaxGeodes(bp, state with { }, max));
            }

            Ans(input.Select(bp => bp.id * MaxGeodes(bp, new State(0, 0, 0, 0, 1, 0, 0, 0, 24))).Sum());
            #endregion Part 1

            #region Part 2
            var ans2 = 1L;
            foreach(var bp in input[..3])
                ans2 *= MaxGeodes(bp, new State(0, 0, 0, 0, 1, 0, 0, 0, 32));
            Ans2(ans2);
            #endregion
        }
    }
}

