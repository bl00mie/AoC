﻿using System;
using System.Linq;

namespace AoC._2021._24
{
    class Program : ProgramBase
    {
        static string[][] cmds;
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
            Ans(monad(0,0,0,0,0));
            cacher.Clear();
            d1 = 1; d2 = 10; delta = 1;
            Ans2(monad(0,0,0,0,0));
        }

        // Total hack, borrowed from Neal Wu's solution. 9_000_000 worked for me.
        // Lower = faster, but raising might fix incorrect answers.
        const int Z = 9_000_000;
        static int d1;
        static int d2;
        static int delta;
        static Cacher<int, int, int, int, int, (bool, string)> cacher = Cacher.Wrap<int, int, int, int, int, (bool, string)>(MONAD);
        static Func<int, int, int, int, int, (bool, string)> monad = cacher.Invoke;

        static (bool success, string val) MONAD(int p, int w, int x, int y, int z)
        {
            if (z > Z)
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
                    var (success, val) = monad(p+1, vars[0], vars[1], vars[2], vars[3]);
                    if (success)
                    {
                        var answer = (true, $"{d}{val}");
                        //WL($"{p} {w} {x} {y} {z} {answer}");
                        return answer;
                    }
                }
                return (false, "");
            }
            else
            {
                var b = char.IsLetter(cmd[2][0]) ? vars[cmd[2][0]-'w'] : int.Parse(cmd[2]);
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
            return monad(p + 1, vars[0], vars[1], vars[2], vars[3]);
        }
    }

}

