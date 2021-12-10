using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021._10
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2021, 10).ToList();

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            var points = 0;
            var scores = new List<long>();

            foreach (var line in input)
            {
                var stack = new Stack<char>();
                var valid = true;
                foreach (char c in line)
                {
                    if ("([{<".Contains(c))
                        stack.Push(c);
                    else
                    {
                        var opener = c == ')' ? c - 1 : c - 2;
                        if (stack.Peek() == opener)
                            stack.Pop();
                        else
                        {
                            points += c switch
                            {
                                ')' => 3,
                                ']' => 57,
                                '}' => 1197,
                                '>' => 25137,
                                _ => throw new NotImplementedException()
                            };
                            valid = false;
                            break;
                        }
                    }
                }
                if (valid)
                {
                    long score = 0;
                    while (stack.Count > 0)
                    {
                        score *= 5;
                        score += stack.Pop() switch
                        {
                            '(' => 1,
                            '[' => 2,
                            '{' => 3,
                            '<' => 4,
                            _ => throw new NotImplementedException()
                        };
                    }
                    scores.Add(score);
                }
            }
            Ans(points);
            scores.Sort();
            Ans2(scores[scores.Count / 2]);
        }
    }
}

