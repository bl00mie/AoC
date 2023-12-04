using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2023._3
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 3).ToList();
            var engine = new Grid<char>(input);

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var ans = 0;
            var ans2 = 0;
            var nums = new HashSet<List<(Coord p, char v)>>();
            for (int y = 0; y < input.Count; y++)
            {
                var num = new List<(Coord p, char v)>();
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (char.IsDigit(engine[x, y]))
                    {
                        num.Add((new Coord(x, y), engine[x, y]));
                        if (x+1 == input[0].Length)
                        {
                            nums.Add(num);
                            num = new();
                        }
                    }
                    else if (num.Count != 0)
                    {
                        nums.Add(num);
                        num = new();
                    }
                }    
            }

            var gears = new Dictionary<Coord, List<int>>();
            foreach (var num in nums)
            {
                foreach ((Coord p, char v) in num)
                {
                    var nb = engine.Neighbors(p, GridVector.DirsAll, ((Coord p, char v) mine, (Coord p, char v) theirs) => !char.IsDigit(theirs.v) && theirs.v != '.');
                    if (nb.Any())
                    {
                        var val = num[0].v - '0';
                        for (int i = 1; i < num.Count; i++)
                        {
                            val *= 10;
                            val += num[i].v - '0';
                        }
                        ans += val;
                        var sym = nb.First();
                        if (sym.v == '*')
                        {
                            if (!gears.ContainsKey(sym.p))
                                gears[sym.p] = new();
                            gears[sym.p].Add(val);
                        }
                        break;
                    }
                }
            }
            var twos = gears.Values.Where(nums => nums.Count == 2);
            ans2 = twos.Sum(l => l.Aggregate((a, b) => a * b));

            Ans(ans);
            #endregion Part 1

            #region Part 2

            Ans2(ans2);
            #endregion
        }
    }
}

