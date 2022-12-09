using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2022._9
{
    class Program : ProgramBase
    {
        static void Main()
        {
            var moves = AoCUtil.GetAocInput(2022, 9)
                .Select(line => line.Split(' '))
                .Select(sa => (sa[0][0], int.Parse(sa[1]))).ToArray();

            var dirs = new Dictionary<char, GridVector>
            {
                ['U'] = GridVector.N,
                ['D'] = GridVector.S,
                ['R'] = GridVector.E,
                ['L'] = GridVector.W
            };

            var knots = new Coord[10] { new(0, 0), new(0, 0), new(0, 0), new(0, 0), new(0, 0), new(0, 0), new(0, 0), new(0, 0), new(0, 0), new(0, 0) };
            var positions = new HashSet<Coord> { new(knots[0]) };
            var positions2 = new HashSet<Coord> { new(knots[0]) };

            foreach (var (d, c) in moves)
                foreach(var _ in Enumerable.Range(0, c))
                {
                    knots[0] += dirs[d];
                    for (int i = 1; i < 10; i++)
                        knots[i] = MoveTail(knots[i-1], knots[i]);
                    positions.Add(new(knots[1]));
                    positions2.Add(new(knots[9]));
                }
            Ans(positions.Count);
            Ans2(positions2.Count);
        }

        static Coord MoveTail(Coord H, Coord T)
        {
            var dx = Math.Abs(H.x - T.x);
            var dy = Math.Abs(H.y - T.y);
            if (dx <= 1 && dy <= 1)
                return new(T);
            if (dy == 0)
                return T + (T.x < H.x ? GridVector.E : GridVector.W);
            if (dx == 0)
                return T + (T.y < H.y ? GridVector.N : GridVector.S);
            if (T.x < H.x)
                return T + (T.y < H.y ? GridVector.NE : GridVector.SE);
            return T + (T.y < H.y ? GridVector.NW : GridVector.SW);
        }
    }
}

