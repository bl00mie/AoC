using System.Linq;

namespace AoC._2021._06
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2021, 06).First().GetLongs();
            var buckets = input.GroupBy(x => x).ToDictionary(group => group.Key, group => (long)group.Count());
            for (int i = 0; i < 9; i++)
                if (!buckets.ContainsKey(i))
                    buckets[i] = 0;
            #endregion

            #region Part 1
            for (int _ = 0; _ < 80; _++)
            {
                var newFish = buckets[0];
                for (int i = 0; i < 8; i++)
                    buckets[i] = buckets[i + 1];
                buckets[8] = newFish;
                buckets[6] += newFish;
            }
            Ans(buckets.Select(kv => kv.Value).Sum());
            #endregion Part 1

            #region Part 2
            for (int _ = 80; _ < 256; _++)
            {
                var newFish = buckets[0];
                for (int i = 0; i < 8; i++)
                    buckets[i] = buckets[i + 1];
                buckets[8] = newFish;
                buckets[6] += newFish;
            }
            Ans(buckets.Select(kv => kv.Value).Sum(), 2);
            #endregion
        }
    }
}

