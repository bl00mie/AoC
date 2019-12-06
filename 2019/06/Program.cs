using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2019._06
{
    class Program
    {
        static void Main()
        {
            var lines = AoCUtil.GetAocInput(2019, 6);

            Dictionary<string, Tuple<string, List<string>>> sats = new Dictionary<string, Tuple<string, List<string>>>();

            var data = lines.Select(line => {
                string[] sa = line.Split(')');
                return (sa[0], sa[1]);
            }).ToArray();

            foreach ((string orbitee, string orbiter) in data)
            {
                if (!sats.ContainsKey(orbiter))
                {
                    sats[orbiter] = new Tuple<string, List<string>>(orbitee, new List<string>());
                }
                else
                {
                    sats[orbiter] = new Tuple<string, List<string>>(orbitee, sats[orbiter].Item2);
                }

                if (!sats.ContainsKey(orbitee))
                {
                    sats[orbitee] = new Tuple<string, List<string>>(null, new List<string>());
                }
                sats[orbitee].Item2.Add(orbiter);
            }

            long tot = 0;

            foreach (var sat in sats)
            {
                if (sat.Key == "COM") continue;
                tot += walk1(sats, sat.Value.Item1) + 1;
            }

            Console.WriteLine(tot);



            string target = sats["SAN"].Item1;

            Console.WriteLine(walk2(sats, sats["YOU"].Item1, target, null));

        }

        static long walk1(Dictionary<string, Tuple<string,List<string>>> satellites, string key)
        {
            if (key == "COM") return 0;
            long tot = 0;
            var sat = satellites[key];
            while (true)
            {
                tot++;
                if (sat.Item1 == "COM") break;
                sat = satellites[sat.Item1];
            }
            return tot;
        }

        static int walk2(Dictionary<string, Tuple<string, List<string>>> satellites, string currKey, string tarKey, string prevKey)
        {
            if (currKey == null)
            {
                return -1;
            }
            var curr = satellites[currKey];
            if (curr.Item1 == tarKey)
            {
                return 0;
            }
            if (curr.Item2.Contains(tarKey))
            {
                return 1;
            }

            int min = int.MaxValue;

            if (curr.Item1 != prevKey)
            {
                int d = walk2(satellites, curr.Item1, tarKey, currKey);
                if (d >= 0 && d < min) min = d;
            }
            foreach (string outkey in curr.Item2)
            {
                if (outkey == prevKey)
                {
                    continue;
                }
                int d = walk2(satellites, outkey, tarKey, currKey);
                if (d >= 0 && d < min) min = d;
            }

            if (min == int.MaxValue) return -1;
            return min + 1;
        }

    }
}
