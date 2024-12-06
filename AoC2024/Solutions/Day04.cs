using AoCUtil.Collections;

namespace AoC2024.Solutions
{
    public class Day04() : BaseDay(2024)
    {
        Grid<char> Grid = new();
        public override void ProcessInput()
        {
            Grid = new Grid<char>(Input);
        }

        public override dynamic Solve_1()
        {
            var dirs = GridVector.DirsAll;
            var xmas = 0;
            foreach (var p in Grid.Where(pair => pair.v == 'X'))
                foreach (var dir in dirs)
                    if (Grid[p.p + dir] == 'M' && Grid[p.p + dir * 2] == 'A' && Grid[p.p + dir * 3] == 'S')
                        xmas++;
            return xmas;
        }

        public override dynamic Solve_2()
        {
            var masx = 0;
            foreach (var p in Grid.Where(pair => pair.v == 'A'))
            {
                if (((Grid[p.p + GridVector.NW] == 'M' && Grid[p.p + GridVector.SE] == 'S') ||
                     (Grid[p.p + GridVector.NW] == 'S' && Grid[p.p + GridVector.SE] == 'M'))
                   &&
                    ((Grid[p.p + GridVector.NE] == 'M' && Grid[p.p + GridVector.SW] == 'S') ||
                     (Grid[p.p + GridVector.NE] == 'S' && Grid[p.p + GridVector.SW] == 'M')))
                    masx++;
            }
            return masx;
        }
    }
}
