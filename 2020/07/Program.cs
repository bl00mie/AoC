using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2020._07
{
    class Program : ProgramBase
    {

        static Dictionary<string, List<string>> containedIn = new();
        static Dictionary<string, Dictionary<string, int>> contains = new();

        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2020, 07).ToList();
            #endregion

            #region Part 1
            foreach (var line in input)
            {
                var (key, contents) = line.Extract<(string, string)> (@"^(.*) bags contain (.*)\.$");
                if (contents.Contains("no other")) continue;
                contains[key] = new();
                foreach (var content in contents.Split(","))
                {
                    var (count, color) = content.Trim().Extract<(int, string)>(@"^(\d+)? (.*) bags?$");
                    if (!containedIn.ContainsKey(color)) containedIn[color] = new();
                    containedIn[color].Add(key);
                    contains[key][color] = count;
                }
            }
            HashSet<string> containers = new();
            tally("shiny gold", containers);

            Ans(containers.Count-1);
            #endregion Part 1

            #region Part 2
            Ans2(tally2("shiny gold")-1);
            #endregion
        }

        static void tally(string key, HashSet<string> containers)
        {
            if (containers.Contains(key)) return;
            containers.Add(key);
            foreach (var color in containedIn[key])
            {
                if (!containedIn.ContainsKey(color))
                {
                    containers.Add(color);
                    continue;
                }
                tally(color, containers);
            }
        }

        private static int tally2(string key)
        {
            int val = 1;
            if (contains.ContainsKey(key))
                foreach (var bag in contains[key])
                   val += bag.Value * tally2(bag.Key);
            return val;
        }
    }
}

