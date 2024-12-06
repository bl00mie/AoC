using AoCUtil.Collections;

namespace AoC2024.Solutions
{
    public class Day06() : BaseDay(2024)
    {
        Grid<char> Grid = new();
        readonly HashSet<Coord> Visited = [];
        public override void ProcessInput()
        {
            Grid = new Grid<char>(Input);
        }

        public override dynamic Solve_1()
        {
            var dirs = GridVector.DirsURDL.ToArray();
            var d = 0;
            var p = Grid.Where(pair => pair.v == '^').Single().p;
            while (Grid.ContainsCoord(p))
            {
                if (Grid[p + dirs[d]] == '#')
                    d = (d + 1)%4;
                else
                {
                    Visited.Add(p);
                    p = p + dirs[d];
                }
            }
            return Visited.Count;
        }

        public override dynamic Solve_2()
        {
            var dirs = GridVector.DirsURDL.ToArray();
            var loop = 0;
            foreach (var O in Visited)
            {
                var d = 0;
                var p = Grid.Where(pair => pair.v == '^').Single().p;
                var path = new HashSet<(Coord p, GridVector dir)>();
                while (Grid.ContainsCoord(p))
                {
                    if (path.Contains((p, dirs[d])))
                    {
                        loop++;
                        break;
                    }
                    path.Add((p, dirs[d]));
                    var n = p + dirs[d];
                    if (n == O || Grid[n] == '#')
                        d = (d + 1) % 4;
                    else
                        p = n;
                }
            }
            return loop;
        }
    }
}
