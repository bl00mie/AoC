using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2015._05
{
    class Program
    {
        static void Main()
        {
            var input = AoCUtil.GetAocInput(2015, 5).ToArray();

            #region Part 1

            var vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
            var bad = new HashSet<string>() { "ab", "cd", "pq", "xy" };
            int nice = 0;
            foreach(var s in input)
            {
                int vc = vowels.Contains(s[0]) ? 1 : 0;
                int dub = 0;
                for (int i=1; i<s.Length; i++)
                {
                    if (vowels.Contains(s[i])) vc++;
                    if (s[i] == s[i-1]) dub++;
                    if (bad.Contains(s.Substring(i-1, 2)))
                    {
                        vc = 0;
                        break;
                    }
                }
                if (vc >= 3 && dub >= 1) nice++;
            }
            Console.WriteLine(nice);

            #endregion Part 1

            nice = 0;
            foreach (var s in input)
            {
                bool xyxy = false;
                bool xyx = false;
                for (int i=0; i<s.Length-2; i++)
                {
                    xyx |= s[i] == s[i + 2];
                    xyxy |= s.Substring(i + 2).Contains(s.Substring(i, 2));
                    if (xyx && xyxy) break;
                }
                if (xyx && xyxy) nice++;
            }
            Console.WriteLine(nice);

            #region Part 2

            #endregion
        }
    }
}
