using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RegExtract;

namespace AoC._2020._04
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            
            var input = AoCUtil.GetAocInput(2020, 04).ToList();
            input.Add("");

            #endregion
            var ecls = new HashSet<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            int valid1 = 0;
            int valid2 = 0;
            Dictionary<string, string> p = new();

            foreach (var line in input)
            {
                if ("".Equals(line))
                {
                    if (isValid(p)) {
                        valid1++;
                        long byr = long.Parse(p["byr"]),
                             iyr = long.Parse(p["iyr"]),
                             eyr = long.Parse(p["eyr"]);

                        bool hgtPass = false;
                        var match = new Regex(@"^(?<val>\d{2,3})(?<unit>[cmin]{2})$").Match(p["hgt"]);
                        if (match.Success)
                        {
                            long h = long.Parse(match.Groups["val"].Value);
                            var u = match.Groups["unit"].Value;
                            hgtPass =
                                (u == "cm" && 150 <= h && h <= 193) ||
                                (u == "in" &&  59 <= h && h <=  76);
                        }

                        if (
                            (1920 <= byr && byr <= 2002) &&
                            (2010 <= iyr && iyr <= 2020) &&
                            (2020 <= eyr && eyr <= 2030) &&
                            hgtPass &&
                            new Regex(@"^#[0-9a-f]{6}$").IsMatch(p["hcl"]) &&
                            ecls.Contains(p["ecl"]) &&
                            new Regex(@"^\d{9}$").IsMatch(p["pid"])
                         ) valid2++;
                    }
                    p = new();
                    continue;
                }
                foreach(var item in line.Split(" "))
                {
                    (string key, string val) = item.Extract<(string, string)>(@"^(.+):(.+)$");
                    p.Add(key, val);
                }
            }

            Ans(valid1);

            Ans(valid2, 2);
        }

        static bool isValid( Dictionary<string,string> p)
        {
            foreach (var key in new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" })
            {
                if (!p.ContainsKey(key)) return false;
            }
            return true;
        }
    }
}

