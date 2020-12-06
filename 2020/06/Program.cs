using System.Collections.Generic;
using System.Linq;

namespace AoC._2020._06
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2020, 06).ToList<string>();
            input.Add("");
            #endregion

            int tot = 0;
            int tot2 = 0;
            int ppl = 0;
            var qns = new Dictionary<char, int>();
            foreach (var line in input)
            {
                if ("".Equals(line))
                {
                    tot += qns.Count();
                    tot2 += qns.Values.Where(x => x == ppl).Count();
                    qns = new();
                    ppl = 0;
                }
                else
                {
                    ppl++;
                    foreach (var c in line)
                        if (qns.ContainsKey(c)) qns[c]++;
                        else qns[c] = 1;
                }
            }
            Ans(tot);
            Ans2(tot2);
        }
    }
}

