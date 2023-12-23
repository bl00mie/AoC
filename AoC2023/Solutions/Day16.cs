using AoCUtil.Collections;

namespace AoC2023.Solutions
{
    internal class Day16 : BaseDay2023
    {
        Grid<char> grid = new();
        public override void ProcessInput()
        {
            grid = grid = new Grid<char>(Input);
        }

        int FirinMahLazer((Coord p, GridVector dir) start)
        {
            var energized = new HashSet<(Coord, GridVector)>();
            var beams = new HashSet<(Coord p, GridVector dir)> { start };
            var split = new Dictionary<char, GridVector[]>
            {
                ['|'] = [GridVector.N, GridVector.S],
                ['-'] = [GridVector.E, GridVector.W]
            };
            while (beams.Count > 0)
            {
                var newbeams = new HashSet<(Coord p, GridVector dir)>();
                foreach (var (p, dir) in beams)
                {
                    var n = p + dir;
                    if (!grid.ContainsCoord(n) || energized.Contains((n, dir)))
                        continue;
                    energized.Add((n, dir));
                    var v = grid[n];
                    if (v == '.')
                        newbeams.Add((n, dir));
                    else if (split.TryGetValue(v, out var splitDirs))
                    {
                        if (splitDirs.Contains(dir))
                            newbeams.Add((n, dir));
                        else
                            foreach (var dir2 in splitDirs)
                                newbeams.Add((n, dir2));
                    }
                    else
                    {
                        var newdir = v switch
                        {
                            '\\' => (dir.dx, dir.dy) switch
                            {
                                (1, 0) => GridVector.N,
                                (-1, 0) => GridVector.S,
                                (0, 1) => GridVector.E,
                                (0, -1) => GridVector.W,
                                _ => throw new Exception("\\ fuuuu")
                            },
                            '/' => (dir.dx, dir.dy) switch
                            {
                                (1, 0) => GridVector.S,
                                (-1, 0) => GridVector.N,
                                (0, 1) => GridVector.W,
                                (0, -1) => GridVector.E,
                                _ => throw new Exception("/ fuuuu")
                            },
                            _ => throw new Exception("fuuuu")
                        };
                        newbeams.Add((n, newdir));
                    }
                }
                beams = newbeams;
            }

            return energized.Select(tuple => tuple.Item1).ToHashSet().Count;
        }

        public override dynamic Solve_1()
        {
            return FirinMahLazer((new Coord(-1, 0), GridVector.E));
        }

        public override dynamic Solve_2()
        {
            var max = 0;
            foreach(var x in Enumerable.Range(0, grid.W))
            {
                max = Math.Max(max, FirinMahLazer((new(x, -1), GridVector.N)));
                max = Math.Max(max, FirinMahLazer((new(x, grid.H), GridVector.S)));
            }
            foreach(var y in Enumerable.Range(0, grid.H))
            {
                max = Math.Max(max, FirinMahLazer((new(-1, y), GridVector.E)));
                max = Math.Max(max, FirinMahLazer((new(grid.W, y), GridVector.W)));
            }
            return max;
        }
    }
}
