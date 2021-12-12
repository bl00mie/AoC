using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2021._12
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2021, 12).Extract<(string, string)>(@"(\w+)-(\w+)");
            var paths = new Dictionary<string, List<string>>();
            foreach (var pair in input)
            {
                if (!paths.ContainsKey(pair.Item1)) paths.Add(pair.Item1, new List<string>());
                if (!paths.ContainsKey(pair.Item2)) paths.Add(pair.Item2, new List<string>());
                paths[pair.Item2].Add(pair.Item1);
                paths[pair.Item1].Add(pair.Item2);
            }
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            int success = 0;
            var attempts = new List<List<string>> { new List<string>(new[] { "start" }) };
            while(attempts.Any())
            {
                var attempt = attempts[^1];
                attempts.RemoveAt(attempts.Count-1);
                if (attempt[^1] == "end")
                {
                    success++;
                    continue;
                }
                foreach (var allowed in paths[attempt[^1]].Where(path => char.IsUpper(path[0]) || !attempt.Contains(path)))
                {
                    var newAttempt = new List<string>(attempt);
                    newAttempt.Add(allowed);
                    attempts.Add(newAttempt);
                }
            }
            Ans(success);
            #endregion Part 1

            #region Part 2
            success = 0;
            attempts = new List<List<string>> { new List<string>(new[] { "start" }) };
            while (attempts.Any())
            {
                var attempt = attempts[^1];
                attempts.RemoveAt(attempts.Count - 1);
                if (attempt[^1] == "end")
                {
                    success++;
                    continue;
                }
                foreach (var path in paths[attempt[^1]])
                {
                    if (path == "start") continue;
                    if (char.IsLower(path[0]))
                        if (attempt.Contains(path))
                        {
                            if (attempt.Where(s => char.IsLower(s[0])).GroupBy(s => s).Where(group => group.Count() == 2).Any())
                                continue;
                        }
                    var newAttempt = new List<string>(attempt);
                    newAttempt.Add(path);
                    attempts.Add(newAttempt);
                }
            }
            Ans2(success);
            #endregion
        }
    }
}

