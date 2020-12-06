using RegExtract;

namespace AoC._2020._02
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2020, 02);
            #endregion

            #region Part 1

            int good = 0;
            int good2 = 0;
            foreach (var s in input)
            {
                (int lo, int hi, char valid, string passwd) = s.Extract<(int, int, char, string)>(@"^(\d+)-(\d+) (.): (.+)$");

                int count = 0;
                foreach (char c in passwd)
                    if (c == valid)
                        count++;
                if (count <= hi && count >= lo) good++;

                good2 += (passwd[lo - 1] == valid) ^ (passwd[hi - 1] == valid) ? 1 : 0;
            }

            Ans(good);
            #endregion Part 1


            #region Part 2

            Ans(good2, 2);
            #endregion
        }
    }
}

