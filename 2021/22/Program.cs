using System;
using System.Collections.Generic;
using System.Linq;
using AoCUtil.Collections;
using RegExtract;

namespace AoC._2021._22
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2021, 22)
                .Extract<(string cmd, int x1, int x2, int y1, int y2, int z1, int z2)>(@"(\w+) x=(-?\w+)..(-?\w+),y=(-?\w+)..(-?\w+),z=(-?\w+)..(-?\w+)")
                .ToList();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var allCubes = new DefaultDictionary<(int x, int y, int z), bool>(false, false);
            foreach (var (cmd, x1, x2, y1, y2, z1, z2) in input)
            {
                var cb = new Cuboid(x1,x2, y1, y2, z1, z2);
                if (cb.x1 > 50 || cb.y1 > 50 || cb.z1 > 50 || cb.x2 < -50 || cb.y2 < -50 || cb.z2 < -50)
                    continue;

                var val = cmd == "on";
                for (int x = cb.x1; x <= cb.x2; x++)
                    for (int y = cb.y1; y <= cb.y2; y++)
                        for (int z = cb.z1; z <= cb.z2; z++)
                            allCubes[(x, y, z)] = val;
            }
            Ans(allCubes.Where(pair => pair.Value).Count());
            #endregion Part 1

            #region Part 2
            List<Cuboid> core = new();
            foreach (var (cmd, x1, x2, y1, y2, z1, z2) in input)
            {
                var c = new Cuboid(x1, x2, y1, y2, z1, z2);
                if (cmd == "on")
                {
                    var cParts = new List<Cuboid> { c };
                    foreach (var coreCube in core)
                    {
                        var newParts = new List<Cuboid>();
                        foreach (var part in cParts)
                            newParts.AddRange(Subtract(part, coreCube));
                        cParts = newParts;
                    }
                    core.AddRange(cParts);
                }
                else
                {
                    var newCore = new List<Cuboid>();
                    foreach (var coreCube in core)
                        newCore.AddRange(Subtract(coreCube, c));
                    core = newCore;
                }
            }
            Ans2(core.Sum(on => on.volume));
            #endregion
        }

        static IEnumerable<Cuboid> Subtract(Cuboid a, Cuboid b)
        {
            if ((a.x1 > b.x2 || b.x1 > a.x2) || (a.y1 > b.y2 || b.y1 > a.y2) || (a.z1 > b.z2 || b.z1 > a.z2))
                return new Cuboid[] { a };

            if (a.x1 >= b.x1 && a.x2 <= b.x2 && a.y1 >= b.y1 && a.y2 <= b.y2 && a.z1 >= b.z1 && a.z2 <= b.z2)
                return Enumerable.Empty<Cuboid>();

            var parts = new List<Cuboid>();

            if (a.x1 < b.x1)
                parts.Add(new Cuboid(a.x1, b.x1 - 1, a.y1, a.y2, a.z1, a.z2));
            if (a.x2 > b.x2)
                parts.Add(new Cuboid(b.x2 + 1, a.x2, a.y1, a.y2, a.z1, a.z2));
            var X1 = Math.Max(a.x1, b.x1);
            var X2 = Math.Min(a.x2, b.x2);
            if (a.y1 < b.y1)
                parts.Add(new Cuboid(X1, X2, a.y1, b.y1 - 1, a.z1, a.z2));
            if (a.y2 > b.y2)
                parts.Add(new Cuboid(X1, X2, b.y2 + 1, a.y2, a.z1, a.z2));
            var Y1 = Math.Max(a.y1, b.y1);
            var Y2 = Math.Min(a.y2, b.y2);
            if (a.z1 < b.z1)
                parts.Add(new Cuboid(X1, X2, Y1, Y2, a.z1, b.z1 - 1));
            if (a.z2 > b.z2)
                parts.Add(new Cuboid(X1, X2, Y1, Y2, b.z2 + 1, a.z2));

            return parts;
        }

        record struct Cuboid
        {
            public int x1;
            public int x2;
            public int y1;
            public int y2; 
            public int z1;
            public int z2;

            public Cuboid(int x1, int x2, int y1, int y2, int z1, int z2)
            {
                this.x1 = Math.Min(x1, x2); this.x2 = Math.Max(x1, x2);
                this.y1 = Math.Min(y1, y2); this.y2 = Math.Max(y1, y2);
                this.z1 = Math.Min(z1, z2); this.z2 = Math.Max(z1, z2);
            }

            public long volume => (x2 - x1 + 1L) * (y2 - y1 + 1L) * (z2 - z1 + 1L);
        }
    }
}

