using System;
using System.Collections.Generic;
using System.Linq;
using Nito.Collections;
using RegExtract;

namespace AoC._2022._21
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = new Deque<(string n, string expr)>(AoCUtil.GetAocInput(2022, 21)
                .Select(l => l.Split(": "))
                .Select(sa => (sa[0], sa[1]))
                .ToList<(string n, string expr)>());
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            long domath(long a, long b, string op)
            {
                return op switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    _ => -1
                };
            }

            Dictionary<string, long> monkeys = new();
            while(input.Any())
            {
                var (n, expr) = input.First(); input.RemoveFromFront();
                if (char.IsDigit(expr[0]))
                    monkeys[n] = long.Parse(expr);
                else
                {
                    var a = expr.Split(' ');
                    if (monkeys.ContainsKey(a[0]) && monkeys.ContainsKey(a[2]))
                        monkeys[n] = domath(monkeys[a[0]], monkeys[a[2]], a[1]);
                    else input.AddToBack((n, expr));
                }
            }

            Ans(monkeys["root"]);
            #endregion Part 1

            #region Part 2

            //Ans("", 2);
            #endregion
        }
    }
}

