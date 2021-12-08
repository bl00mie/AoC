using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021._08
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion
            var input = AoCUtil.GetAocInput(2021, 08)
                .Select(line => line.Split(" | "))
                .Select(sa => (sa[0].Split(' '), sa[1].Split(' ')));
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            int _1478 = input.Select(pair => pair.Item2.Where(d => d.Length != 5 && d.Length != 6).Count()).Sum();
            Ans(_1478);
            #endregion Part 1

            #region Part 2
            int total = 0;
            foreach ((var patterns, var outputs) in input)
            {
                var map = new char['h'];

                var one = patterns.Where(p => p.Length == 2).Single();
                var three = patterns.Where(p => p.Length == 5).Where(p => p.Contains(one[0]) && p.Contains(one[1])).Single();
                var four = patterns.Where(p => p.Length == 4).Single();
                var lSix = patterns.Where(p => p.Length == 6);
                var six = lSix.Where(p => p.Intersect(one).Count() == 1).Single();
                var seven = patterns.Where(p => p.Length == 3).Single();
                var zero = lSix.Where(p =>
                {
                    var data = four.Except(p);
                    if (!data.Any()) return false;
                    return !one.Contains(data.Single());
                }).Single();
                var nine = lSix.Except(new string[] { six, zero }).Single();

                map[seven.Except(one).Single()] = 'a';
                map[four.Except(three).Single()] = 'b';
                map[one.Except(six).Single()] = 'c';
                map[four.Except(zero).Single()] = 'd';
                map[six.Except(nine).Single()] = 'e';
                map[one.Intersect(six).Single()] = 'f';
                map[nine.Except(four.Union(seven)).Single()] = 'g';

                total += (new string(outputs.Select(s => s.Select(c => map[c])).Select(s => SS2Digit(s)).ToArray())).Int();
            }
            Ans(total, 2);
            #endregion
        }

        static char SS2Digit(IEnumerable<char> ss)
        {
            var ssArr = ss.ToArray();
            Array.Sort(ssArr);
            return ssArr.Length switch
            {
                2 => '1',
                3 => '7',
                4 => '4',
                7 => '8',
                _ => (new string(ssArr) switch
                {
                    "abcefg" => '0',
                    "acdeg"  => '2',
                    "acdfg"  => '3',
                    "abdfg"  => '5',
                    "abdefg" => '6',
                    "abcdfg" => '9',
                    _        => throw new Exception("uh oh")
                })
            };
        }
    }
}

