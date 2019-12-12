using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2019._12
{
    class Program : ProgramBase
    {
        const int X = 0;
        const int Y = 1;
        const int Z = 2;

        static void Main()
        {
            Regex rx = new Regex(@"^<x=(?<x>-?\d+), y=(?<y>-?\d+), z=(?<z>-?\d+)>");

            var P = (from L in AoCUtil.GetAocInput(2019, 12)
                    let m = rx.Match(L)
                    select new int[] {
                        int.Parse(m.Groups["x"].Value),
                        int.Parse(m.Groups["y"].Value),
                        int.Parse(m.Groups["z"].Value)
                    }).ToList();

            List<int[]> V = new List<int[]>();
            for (int i=0; i<P.Count; i++)
                V.Add(new int[] { 0,0,0} );

            List<HashSet<string>> seen = new List<HashSet<string>>() { new HashSet<string>(), new HashSet<string>(), new HashSet<string>() };
            ulong?[] repeats = { null, null, null };

            for (uint step = 0; ; step++)
            {
                if (step == 1000)
                {
                    long e = 0;
                    for (int moon = 0; moon < P.Count; moon++)
                    {
                        var p = Math.Abs(P[moon][X]) + Math.Abs(P[moon][Y]) + Math.Abs(P[moon][Z]);
                        var k = Math.Abs(V[moon][X]) + Math.Abs(V[moon][Y]) + Math.Abs(V[moon][Z]);
                        e += p * k;
                    }
                    Ans(e);
                }

                if (repeats[X] > 0 && repeats[Y] > 0 && repeats[Z] > 0) break;
                for (int m1 = 0; m1 < P.Count - 1; m1++)
                    for (int j = m1 + 1; j < P.Count; j++)
                        for (int axis = X; axis <= Z; axis++)
                            if (P[m1][axis] < P[j][axis]) { V[m1][axis]++; V[j][axis]--; }
                            else if (P[m1][axis] > P[j][axis]) { V[m1][axis]--; V[j][axis]++; }
                for (int i = 0; i < P.Count; i++)
                    for (int axis = X; axis <= Z; axis++)
                        P[i][axis] += V[i][axis];
                
                for (int axis = X; axis <= Z; axis++)
                {
                    var key = stringify(P, V, axis);
                    if (seen[axis].Contains(key))
                        repeats[axis] ??= step;
                    seen[axis].Add(key);
                }
            }

            Ans(AoCUtil.LCM(AoCUtil.LCM(repeats[X]??1, repeats[Y]??1), repeats[Z]??1), 2);
        }

        static string stringify(List<int[]> P, List<int[]> V, int axis)
        {
            string s = "";
            for (int i = 0; i < P.Count; i++)
                s += string.Format("{0},{1},", P[i][axis], V[i][axis]);
            return s;
        }
    }
}

