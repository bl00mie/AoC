using AoCUtil.Collections;
using NetFabric;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2022._12
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var grid = new Grid<char>(AoCUtil.GetAocInput(2022, 12));
            var S = grid.Where(pair => pair.v == 'S').Single().p;
            grid[S] = 'a';
            var E = grid.Where(pair => pair.v == 'E').Single().p;
            grid[E] = 'z';
            var visited = new HashSet<Coord>();

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var queue = new DoublyLinkedList<(Coord p, int d)>(new[] { (S, 0) });
            while (!queue.IsEmpty)
            {
                var (p, d) = queue.First.Value;
                queue.RemoveFirst();
                if (visited.Contains(p))
                    continue;
                visited.Add(p);
                if (p == E)
                {
                    Ans(d);
                    break;
                }
                queue.AddLast(grid.Neighbors(p, GridVector.DirsESNW, CanStep).Select(n => (n.p, d + 1)));
            }
            #endregion Part 1

            #region Part 2
            queue = new DoublyLinkedList<(Coord p, int d)>(grid.Where(pair => pair.v == 'a').Select(pair => (pair.p, 0)));
            visited = new();
            while (!queue.IsEmpty)
            {
                var (p, d) = queue.First.Value;
                queue.RemoveFirst();
                if (visited.Contains(p))
                    continue;
                visited.Add(p);
                if (p == E)
                {
                    Ans2(d);
                    break;
                }
                queue.AddLast(grid.Neighbors(p, GridVector.DirsESNW, CanStep).Select(n => (n.p, d + 1)));
            }
            #endregion
        }

        static bool CanStep((Coord p, char v) mine, (Coord p, char v) theirs) => theirs.v <= mine.v + 1;
    }
}

