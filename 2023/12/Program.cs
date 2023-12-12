using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2023._12
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 12).Extract<(string springs, string contiguousDamaged)>(@"([\?\.#]+) ([0-9,]+)").ToArray();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            var springChars = new[] { '.', '#' };
            var DP = new Dictionary<(int p, int dp, int sz), long>();
            long f(string spr, List<int> damSz, int p, int dp, int sz)
            {
                var key = (p, dp, sz);
                if (DP.ContainsKey(key))
                    return DP[key];
                if (p == spr.Length)
                {
                    if (dp == damSz.Count && sz == 0)
                        return 1;
                    else if (dp == damSz.Count - 1 && damSz[dp] == sz)
                        return 1;
                    return 0;
                }
                var ans = 0L;
                foreach (var c in springChars)
                {
                    if (spr[p] == c || spr[p] == '?')
                    {
                        if (c == '.')
                        {
                            if (sz == 0)
                                ans += f(spr, damSz, p + 1, dp, 0);
                            else if (sz > 0 && dp < damSz.Count && damSz[dp] == sz)
                                ans += f(spr, damSz, p + 1, dp + 1, 0);
                        }
                        else if (c == '#')
                            ans += f(spr, damSz, p + 1, dp, sz + 1);
                    }
                }
                DP[key] = ans;
                return ans;
            }


            foreach (var part2 in new[] {false, true})
            {
                var ans = 0L;
                foreach (var (springs, congiguousDamaged) in input)
                {
                    var spr = springs;
                    var cd = congiguousDamaged;
                    if (part2)
                    {
                        spr = string.Join('?', new[] { spr, spr, spr, spr, spr });
                        cd = string.Join(',', new[] { cd, cd, cd, cd, cd });
                    }
                    DP.Clear();
                    ans += f(spr, cd.GetInts().ToList(), 0, 0, 0);
                }
                Ans(ans, part2 ? 2 : 1);
            }
        }
    }
}

