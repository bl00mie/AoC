using System.Collections.Immutable;
using AoCUtil.Collections;

namespace AoC2023.Solutions
{
    internal class Day21 : BaseDay2023
    {
        Grid<char> map = new();
        int R;
        int C;
        Coord Start = new(0,0);

        readonly ImmutableArray<int> Steps = [64, 26_501_365];
        const int TileRadius = 3;

        public override void ProcessInput()
        {
            map = new(Input);
            R = map.H;
            C = map.W;
            Start = map.Where(x => x.v == 'S').Single().p;
        }

        public override dynamic Solve_1()
        {
            var poss = new HashSet<Coord> { Start };
            for (int i = 0; i < Steps[0]; i++)
            {
                var next = new HashSet<Coord>();
                foreach (var p in poss)
                    foreach (var n in map.Neighbors(p, GridVector.DirsNESW, (mine, theirs) => theirs.v != '#'))
                        next.Add(n.p);
                poss = next;
            }
            return poss.Count;
        }

        Dictionary<(int tr, int tc, int r, int c), int> CalculateDistances(int startRow, int startColumn, int tileRadius)
        {
            Dictionary<(int tr, int tc, int r, int c), int> D = [];
            DLList<(int tr, int tc, int sr, int sc, int d)> Q = [(0, 0, startRow, startColumn, 0)];
            while (Q.Count > 0)
            {
                var (tr, tc, r, c, d) = Q.PopLeft();
                if (r < 0) { tr--; r += R; }
                if (r >= R) { tr++; r -= R; }
                if (c < 0) { tc--; c += C; }
                if (c >= C) { tc++; c -= C; }

                if (D.ContainsKey((tr, tc, r, c))) continue;
                if (Math.Abs(tr) > tileRadius || Math.Abs(tc) > tileRadius) continue;
                if (r < 0 || r >= R || c < 0 || c >= C || Input[r][c] == '#') continue;
                
                D[(tr, tc, r, c)] = d;
                
                foreach (var v in GridVector.DirsNESW)
                    Q.Add((tr, tc, r + v.dy, c + v.dx, d + 1));
            }
            return D;
        }


        public override dynamic Solve_2()
        {
            var distances = CalculateDistances(Start.y, Start.x, TileRadius);

            Dictionary<(long d, bool corner), long> memo = [];
            long solve (int d, bool corner)
            {
                if (memo.TryGetValue((d, corner), out var val))
                    return val;
                var tiles = (Steps[1] - d) / R;
                var res = 0L;
                foreach (var i in Enumerable.Range(1, tiles))
                    if (d+R*i <= Steps[1] && (d+R*i)%2 == Steps[1] % 2)
                        res += (corner ? (i + 1) : 1);
                memo[(d, corner)] = res;
                return res;
            }

            var ans = 0L;
            for(var r = 0; r < R; r++)
                for(var c = 0; c < C; c++)
                    if (distances.ContainsKey((0,0,r,c)))
                    {
                        var range = Enumerable.Range(TileRadius * -1, 2 * TileRadius + 1).ToArray();
                        foreach (int tr in range)
                            foreach (int tc in range)
                            {
                                var distance = distances[(tr, tc, r, c)];
                                if (distance % 2 == 1 && distance <= Steps[1])
                                    ans += 1;
                                if (Math.Abs(tr) == TileRadius && Math.Abs(tc) == TileRadius)
                                    ans += solve(distance, true);
                                else if (Math.Abs(tr) == 4 || Math.Abs(tc) == 4)
                                    ans += solve(distance, false);
                            }
                    }
            return ans;
        }
    }
}
