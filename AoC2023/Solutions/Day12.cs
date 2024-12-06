using System.Collections.Immutable;
using System.Text;

namespace AoC2023.Solutions
{
    internal class Day12() : BaseDay(2023)
    {
        (string springs, string contiguousDamage)[] input = [];
        readonly ImmutableArray<char> springChars = ['.', '#'];
        readonly Dictionary<(int p, int dp, int sz), long> DP = [];
        public override void ProcessInput()
        {
            input = Input.Extract<(string springs, string contiguousDamaged)>(@"([\?\.#]+) ([0-9,]+)").ToArray();
        }

        long DifferentArrangements(string spr, List<int> damSz, int p, int dp, int sz)
        {
            var key = (p, dp, sz);
            if (DP.TryGetValue(key, out long value))
                return value;
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
                            ans += DifferentArrangements(spr, damSz, p + 1, dp, 0);
                        else if (sz > 0 && dp < damSz.Count && damSz[dp] == sz)
                            ans += DifferentArrangements(spr, damSz, p + 1, dp + 1, 0);
                    }
                    else if (c == '#')
                        ans += DifferentArrangements(spr, damSz, p + 1, dp, sz + 1);
                }
            }
            DP[key] = ans;
            return ans;
        }

        public override dynamic Solve_1()
        {
            var answer = 0L;
            foreach(var(springs, congiguousDamage) in input)
            {
                DP.Clear();
                answer += DifferentArrangements(springs, congiguousDamage.GetInts().ToList(), 0, 0, 0);
            }
            return answer;
        }

        public override dynamic Solve_2()
        {
            var answer = 0L;
            foreach (var (springs, congiguousDamage) in input)
            {
                var spr = new StringBuilder(springs);
                var cd0 = congiguousDamage.GetInts();
                var cd = cd0.ToList();
                foreach (var _ in Enumerable.Range(0,4))
                {
                    spr.Append("?").Append(springs);
                    cd.AddRange(cd0);
                }
                DP.Clear();
                answer += DifferentArrangements(spr.ToString(), cd, 0, 0, 0);
            }
            return answer;
        }
    }
}
