using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2019._03
{
    class Program
    {
        static readonly Dictionary<char, int> DX = new Dictionary<char, int>() { { 'R', 1 }, { 'L', -1 }, { 'U', 0 }, { 'D', 0 } };
        static readonly Dictionary<char, int> DY = new Dictionary<char, int>() { { 'R', 0 }, { 'L', 0 }, { 'U', 1 }, { 'D', -1 } };

        static void Main(string[] args)
        {
            #region Template

            var lines = AoCUtil.getAocInput(2019, 3).ToArray<string>();

            #endregion Template


            #region Problem

            Dictionary<Tuple<int, int>, int> PA = getPoints(lines[0].Split(","));
            Dictionary<Tuple<int, int>, int> PB = getPoints(lines[1].Split(","));

            int minMD = int.MaxValue;
            int minMoves = int.MaxValue;
            foreach (Tuple<int,int> t in PA.Keys)
            {
                if (PB.ContainsKey(t))
                {
                    int d = Math.Abs(t.Item1) + Math.Abs(t.Item2);
                    if (d < minMD) minMD = d;
                    int moves = PA[t] + PB[t];
                    if (moves < minMoves) minMoves = moves;
                }
            }

            Console.WriteLine(minMD + " " + minMoves);

            #endregion
        }

        static Dictionary<Tuple<int,int>, int> getPoints(string[] moves)
        {
            Dictionary<Tuple<int, int>, int> points = new Dictionary<Tuple<int, int>, int>();
            int x = 0, y = 0;
            int distance = 0;
            foreach (string move in moves)
            {
                int dx = DX[move[0]];
                int dy = DY[move[0]];
                foreach (int i in Enumerable.Range(0, int.Parse(move.AsSpan()[1..])))
                {
                    distance++;
                    x += dx; y += dy;
                    Tuple<int, int> t = new Tuple<int, int>(x, y);
                    if (!points.ContainsKey(t))
                    {
                        points.Add(t, distance);
                    }
                }
            }
            return points;
        }
    }
}
