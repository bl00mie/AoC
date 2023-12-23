using AoCUtil.Collections;

namespace AoC2023.Solutions
{
    internal class Day03 : BaseDay2023
    {
        Grid<char> Engine = new();
        public override void ProcessInput()
        {
            Engine = new Grid<char>(Input);
        }

        public override dynamic Solve_1()
        {
            var nums = new HashSet<List<(Coord p, char v)>>();
            for (int y = 0; y < Engine.H; y++)
            {
                var num = new List<(Coord p, char v)>();
                for (int x = 0; x < Engine.W; x++)
                {
                    if (char.IsDigit(Engine[x, y]))
                    {
                        num.Add((new Coord(x, y), Engine[x, y]));
                        if (x + 1 == Engine.W)
                        {
                            nums.Add(num);
                            num = [];
                        }
                    }
                    else if (num.Count != 0)
                    {
                        nums.Add(num);
                        num = [];
                    }
                }
            }

            var ans = 0;
            var gears = new Dictionary<Coord, List<int>>();
            foreach (var num in nums)
            {
                foreach ((Coord p, char v) in num)
                {
                    var nb = Engine.Neighbors(p, GridVector.DirsAll, (mine, theirs) => !char.IsDigit(theirs.v) && theirs.v != '.');
                    if (nb.Any())
                    {
                        var val = num[0].v - '0';
                        for (int i = 1; i < num.Count; i++)
                        {
                            val *= 10;
                            val += num[i].v - '0';
                        }
                        ans += val;
                        var sym = nb.First();
                        if (sym.v == '*')
                        {
                            if (!gears.ContainsKey(sym.p))
                                gears[sym.p] = [];
                            gears[sym.p].Add(val);
                        }
                        break;
                    }
                }
            }
            return ans;
        }

        public override dynamic Solve_2()
        {
            var nums = new HashSet<List<(Coord p, char v)>>();
            for (int y = 0; y < Engine.H; y++)
            {
                var num = new List<(Coord p, char v)>();
                for (int x = 0; x < Engine.W; x++)
                {
                    if (char.IsDigit(Engine[x, y]))
                    {
                        num.Add((new Coord(x, y), Engine[x, y]));
                        if (x + 1 == Engine.W)
                        {
                            nums.Add(num);
                            num = [];
                        }
                    }
                    else if (num.Count != 0)
                    {
                        nums.Add(num);
                        num = [];
                    }
                }
            }

            var gears = new Dictionary<Coord, List<int>>();
            foreach (var num in nums)
            {
                foreach ((Coord p, char v) in num)
                {
                    var nb = Engine.Neighbors(p, GridVector.DirsAll, (mine, theirs) => !char.IsDigit(theirs.v) && theirs.v != '.');
                    if (nb.Any())
                    {
                        var val = num[0].v - '0';
                        for (int i = 1; i < num.Count; i++)
                        {
                            val *= 10;
                            val += num[i].v - '0';
                        }
                        var sym = nb.First();
                        if (sym.v == '*')
                        {
                            if (!gears.ContainsKey(sym.p))
                                gears[sym.p] = [];
                            gears[sym.p].Add(val);
                        }
                        break;
                    }
                }
            }
            var twos = gears.Values.Where(nums => nums.Count == 2);
            return twos.Sum(l => l.Aggregate((a, b) => a * b));
        }
    }
}
