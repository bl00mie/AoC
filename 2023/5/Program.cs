using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2023._5
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GroupInput(2023, 5).ToList();
            var seeds = input[0].First().Split(":")[1].Trim().Split(" ").Select(long.Parse).ToList();
            var partners = new Dictionary<string, string>();
            var maps = new Dictionary<string, List<(long d, long s, long r)>>();

            input.RemoveAt(0);
            foreach (var g in input)
            {
                var pair = g.First().Split(" ")[0].Split("-to-");
                partners[pair[0]] = pair[1];
                maps[pair[1]] = g.Skip(1)
                    .Extract<(long d, long s, long r)>(@"(\d+) (\d+) (\d+)")
                    .OrderBy(x => x.s)
                    .ToList();
            }
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var low = long.MaxValue;
            foreach (var seed in seeds)
            {
                var val = seed;
                var partner = partners["seed"];
                while(partners.ContainsKey(partner))
                {
                    var map = maps[partner];
                    foreach ((long d, long s, long r) in map)
                    {
                        if (s > val)
                        {

                        }
                        if (s <= val && val < s + r)
                        {
//                            if 
                        }
                    }
                }
            }

            Ans(low);
            #endregion Part 1

            #region Part 2

            //Ans2(ans2);
            #endregion
        }
    }
}

