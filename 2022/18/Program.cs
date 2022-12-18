using System;
using System.Collections.Generic;
using System.Linq;
using NetFabric;
using RegExtract;

namespace AoC._2022._18
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2022, 18).Extract<(int x, int y, int z)>(@"(\d+),(\d+),(\d+)").ToHashSet();
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1

            var adjacent = 0;
            foreach (var (x, y, z) in input)
                adjacent += input.Count(d => Math.Abs(x - d.x) + Math.Abs(y - d.y) + Math.Abs(z - d.z) == 1);
            Ans(input.Count * 6 - adjacent);

            #endregion Part 1

            #region Part 2

            HashSet<(int x, int y, int z)> inside = new();
            HashSet<(int x, int y, int z)> outside = new();
            var INFINITY = 1500;

            var ans2 = 0;
            foreach (var (x, y, z) in input)
                foreach (var offset in new int[] {-1,1 })
                    ans2 += Outside((x + offset, y, z))
                        + Outside((x, y + offset, z))
                        + Outside((x, y, z + offset));
            Ans2(ans2);

            int Outside((int x, int y, int z) droplet)
            {
                if (outside.Contains(droplet))
                    return 1;
                if (inside.Contains(droplet))
                    return 0;

                var seen = new HashSet<(int x, int y, int z)>();
                var queue = new DoublyLinkedList<(int x, int y, int z)>(new[] { droplet });
                while (!queue.IsEmpty)
                {
                    var n = queue.First.Value; queue.RemoveFirst();
                    if (input.Contains(n))
                        continue;
                    if (seen.Contains(n))
                        continue;
                    seen.Add(n);
                    if (seen.Count > INFINITY)
                    {
                        foreach (var s in seen)
                            outside.Add(s);
                        return 1;
                    }
                    foreach (var offset in new int[] { -1, 1 })
                    {
                        queue.AddLast((n.x + offset, n.y, n.z));
                        queue.AddLast((n.x, n.y + offset, n.z));
                        queue.AddLast((n.x, n.y, n.z + offset));
                    }
                }
                foreach (var s in seen)
                    inside.Add(s);
                return 0;
            }
            #endregion
        }
    }
}

