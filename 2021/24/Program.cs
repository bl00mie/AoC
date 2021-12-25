using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RegExtract;

namespace AoC._2021._24
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            cmds = AoCUtil.GetAocInput(2021, 24).Select(line => line.Split(' ')).ToArray();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            d1 = 9; d2 = 0; delta = -1;
            Ans(MONAD(0,0,0,0,0));

            d1 = 1; d2 = 10; delta = 1;
            cache = new();
            Ans2(MONAD(0,0,0,0,0));

            #region Part 2

            //Ans("", 2);
            #endregion
        }

        static int d1;
        static int d2;
        static int delta;
        static Dictionary<(int, long, long, long, long), (bool, string)> cache = new ();
        static string[][] cmds;
        static (bool success, string val) MONAD(int p, long w, long x, long y, long z)
        {
            var key = (p, w, x, y, z);
            if (cache.ContainsKey(key))
                return cache[(p, w, x, y, z)];
            if (z > 10000000)
                return (false, "");
            if (p >= cmds.Length)
                return (z == 0, "");
            var vars = new[] { w, x, y, z };
            //WL($"[{w} {x} {y} {z}]");
            var cmd = cmds[p];
            var a = cmd[1][0] - 'w';
            if (cmd[0] == "inp")
            {
                for (int d = d1; d!= d2; d += delta)
                {
                    vars[a] = d;
                    var result = MONAD(p+1, vars[0], vars[1], vars[2], vars[3]);
                    if (result.success)
                    {
                        var answer = (true, $"{d}{result.val}");
                        WL($"{p} {w} {x} {y} {z} {answer}");
                        cache[key] = answer;
                        return (true, $"{d}{result.val}");
                    }
                }
                cache[key] = (false, "");
                return (false, "");
            }
            else
            {
                var b = char.IsLetter(cmd[2][0]) ? vars[cmd[2][0]-'w'] : long.Parse(cmd[2]);
                vars[a] = cmd[0] switch
                {
                    "add" => vars[a] + b,
                    "mul" => vars[a] * b,
                    "div" => vars[a] / b,
                    "mod" => vars[a] % b,
                    "eql" => vars[a] == b ? 1 : 0,
                    _ => throw new Exception("wtaf")
                };
            }
            cache[key] = MONAD(p + 1, vars[0], vars[1], vars[2], vars[3]);
            return cache[(p, w, x, y, z)];
        }
    }

}

