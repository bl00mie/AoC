using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2019._04
{
    class Program
    {
        static void Main(string[] args)
        {
            int c = 0;
            for (int i = 206938; i <= 679128; i++)
            {
                string s = i.ToString();
                if (!hasPairs(s)) continue;
                if (hasDecreasing(s)) continue;
                c++;
            }
            Console.WriteLine(c);
        }

        static bool hasPairs(string s)
        {
            for (int i = 0; i < s.Length - 1; i++)
            {
                int match = 0;
                for (int j=i+1; j<s.Length; j++)
                {
                    if (s[i] != s[j]) break;
                    match++;
                }
                if (match==1)
                {
                    return true;
                }
                i += match; 
            }
            return false;
        }

        static bool hasDecreasing(string s)
        {
            for(int i=1; i<s.Length; i++)
            {
                if (s[i] < s[i - 1]) return true;
            }
            return false;
        }
    }
}