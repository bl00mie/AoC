using AoCUtil.Collections;

namespace AoC2019.Solutions
{
    public class Day04() : BaseDay(2019)
    {
        static int Lo;
        static int Hi;
        public override void ProcessInput()
        {
            var range = Input.Extract<(int lo, int hi)>(@"(\d+)-(\d+)");
            Lo = range.First().lo;
            Hi = range.First().hi;
        }

        public override dynamic Solve_1()
        {
            var success = 0;
            for (int v = Lo; v <= Hi; v++)
            {
                var s = v.ToString();
                var unique = new HashSet<char>(s);
                if (unique.Count == s.Length) continue;
                bool increasing = true;
                foreach (var i in Enumerable.Range(1, s.Length-1))
                {
                    if (s[i] < s[i-1])
                    {
                        increasing = false;
                        break;
                    }    
                }
                if (increasing) success++;
            }
            return success;
        }

        public override dynamic Solve_2()
        {
            var valid = 0;
            for (int v = Lo; v <= Hi; v++)
            {
                var digits = new DefaultDictionary<char, int>(storeOnMissingLookup: true);
                var s = v.ToString();
                foreach (var c in s)
                    digits[c]++;
                if (!digits.ContainsValue(2)) continue;
                bool decreasing = false;
                foreach (var i in Enumerable.Range(1, s.Length - 1))
                    if (s[i] < s[i - 1])
                    {
                        decreasing = true;
                        break;
                    }
                if (!decreasing) valid++;
            }
            return valid;
        }
    }
}
