using RegExtract;

namespace AoC2016.Solutions;

internal class Day01 : BaseDay2016
{
    List<(char turn, int blocks)> steps;
    
    public override void ProcessInput()
    {
        steps = Input.Extract<List<(char, int)>>(@"(([LR])(\d+),? ?)+").First();
    }



    static readonly (int dx, int dy)[] dirs = new[]
    {
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0)
    };

    public override string Solve_1()
    {
        int x = 0;
        int y = 0;
        int dir = 0;
        foreach (var (turn, blocks) in steps)
        {
            dir = (dir + (turn == 'R' ? 1 : 3)) % 4;
            x += (dirs[dir].dx * blocks);
            y += (dirs[dir].dy * blocks);
        }
        return $"{Math.Abs(x) + Math.Abs(y)}";
    }

    public override string Solve_2()
    {
        int x = 0;
        int y = 0;
        int dir = 0;
        var path = new HashSet<(int x, int y)>();
        foreach (var (turn, blocks) in steps)
        {
            dir = (dir + (turn == 'R' ? 1 : 3)) % 4;
            var (dy, dx) = dirs[dir];
            bool done = false;
            foreach (var _ in Enumerable.Range(0, blocks))
            {
                if (dx == 0)
                    y += dy;
                else
                    x += dx;
                if (path.Contains((x, y)))
                {
                    done = true;
                    break;
                }
                path.Add((x, y));
            }
            if (done) break;
        }
        return $"{Math.Abs(x) + Math.Abs(y)}";
    }
}
