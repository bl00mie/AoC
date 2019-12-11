using System;
using System.Collections.Generic;
using System.Linq;
using AoC.VM;


namespace AoC._2019._10
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            
            var input = AoCUtil.GetAocInput(2019, 10).ToArray();
            List<(int X, int Y)> points = new List<(int X, int Y)>();
            for (int y=0; y < input.Count(); y++)
            {
                for (int x=0; x<input[y].Length; x++)
                {
                    if (input[y][x] == '#') points.Add((x, y));
                }
            }

            #endregion

            #region Part 1
            int max = int.MinValue;
            int X = 0;
            int Y = 0;
            foreach (var p in points)
            {
                var ang = new HashSet<double>();
                foreach (var pp in points)
                {
                    if (p == pp) continue;
                    ang.Add(Math.Atan2(p.Y - pp.Y, pp.X - p.X));
                }
                if (ang.Count > max)
                {
                    max = ang.Count;
                    X = p.X; Y = p.Y;
                }
            }
            Ans(max);
            #endregion Part 1

            #region Part 2

            Dictionary<double, List<(int X, int Y, double D)>> angles = new Dictionary<double, List<(int X, int Y, double D)>>();
            foreach (var p in points)
            {
                if (p.X == X && p.Y == Y) continue;
                var angle = Math.Atan2(p.Y - Y, X - p.X); // invert Y
                if (angle > Math.PI / 2.0) // if not in the first quadrant, subtract 2PI so we can order by decreasing and have the right order
                    angle -= Math.PI * 2;
                if (!angles.ContainsKey(angle)) angles[angle] = new List<(int X, int Y, double D)>();
                angles[angle].Add((p.X, p.Y, Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2))));
                angles[angle] = angles[angle].OrderBy(ast => ast.D).ToList();
            }

            int removed = 0;
            while (removed < 200)
            {
                foreach (var angle in angles.Select(a => a).OrderBy(a => a.Key).Reverse())
                {
                    if (angle.Value.Count > 0)
                    {
                        if (++removed == 200)
                        {
                            Ans(angle.Value[0].X * 100 + angle.Value[0].Y, 2);
                            break;
                        }
                        angle.Value.RemoveAt(0);
                    }
                }
            }
            #endregion
        }
    }
}

