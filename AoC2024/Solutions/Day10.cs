using AoCUtil.Collections;
using Nito.Collections;

namespace AoC2024.Solutions
{
    public class Day10() : BaseDay(2024)
    {
        Grid<int> Map = [];
        public override void ProcessInput()
        {
            Map = new Grid<int>(Input.Select(line => line.Select(c => (int)(c - '0')).ToArray()).ToArray());
        }

        bool PlusOne((Coord p, int v) mine, (Coord p, int v) theirs) => theirs.v == mine.v + 1;

        public override dynamic Solve_1()
        {
            var score = 0L;
            var trails = new Deque<Coord>();
            var nines = new HashSet<Coord>();
            foreach (var trailhead in Map.Where(pair => pair.v == 0))
            {
                nines.Clear();
                trails.AddToBack(trailhead.p);
                while(trails.Count > 0)
                {
                    var p = trails.RemoveFromFront();
                    if (Map[p] == 9)
                    {
                        nines.Add(p);
                        continue;
                    }
                    foreach (var n in Map.Neighbors(p, GridVector.DirsNESW, PlusOne))
                        trails.AddToFront(n.p);
                }
                score += nines.Count;
            }
            return score;
        }

        public override dynamic Solve_2()
        {
            var score = 0L;
            foreach (var trailhead in Map.Where(pair => pair.v == 0))
            {
                var trails = new Deque<Coord>();
                trails.AddToBack(trailhead.p);
                while (trails.Count > 0)
                {
                    var p = trails.RemoveFromFront();
                    if (Map[p] == 9)
                    {
                        score++;
                        continue;
                    }
                    foreach (var n in Map.Neighbors(p, GridVector.DirsNESW, PlusOne))
                        trails.AddToFront(n.p);
                }
            }
            return score;
        }
    }
}
