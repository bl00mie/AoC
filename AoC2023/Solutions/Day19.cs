using AoCUtil.Collections;

namespace AoC2023.Solutions
{
    internal class Day19 : BaseDay2023
    {
        Dictionary<string, List<string>> workflows = [];
        List<Dictionary<string, int>> parts = [];
        public override void ProcessInput()
        {
            var groups = AoC.AoCUtil.GroupInput(Input);


            workflows = groups.First()
                .Extract<(string key, List<string> rules)>(@"(\w+){(([0-9a-zRA<>:]+),?)+}")
                .ToDictionary(pair => pair.key, pair => pair.rules);

            parts = groups.Last()
                .Extract<List<(string key, int val)>>(@"{((\w+)=(\d+),?)+}")
                .Select(part => part.ToDictionary(pair => pair.key, pair => pair.val))
                .ToList();
        }

        long Eval(Dictionary<string, int> part)
        {
            var wf = "in";
            while (true)
                foreach (var rule in workflows[wf])
                {
                    var applies = true;
                    var result = rule;
                    var test = rule.Split(':');
                    if (test.Length == 2)
                    {
                        var cat = test[0][..1];
                        var op = test[0][1];
                        result = test[1];
                        var v = int.Parse(test[0][2..]);
                        if (op == '>')
                            applies = part[cat] > v;
                        else
                            applies = part[cat] < v;
                    }
                    if (applies)
                    {
                        if (result == "A") return part.Values.Sum();
                        if (result == "R") return 0;
                        wf = result;
                        break;
                    }
                }
        }

        public override dynamic Solve_1() => parts.Sum(Eval);

        static (int lo, int hi) AdjustRange(string op, int v, int lo, int hi)
            =>  op switch
            {
                ">" => (Math.Max(lo, v + 1), hi),
                "<" => (lo, Math.Min(hi, v - 1)),
                ">=" => (Math.Max(lo, v), hi),
                "<=" => (lo, Math.Min(hi, v)),
                _ => throw new Exception("fuuu")
            };

        static (int xl, int xh, int ml, int mh, int al, int ah, int sl, int sh) AdjustRanges(char cat, string op, int v, int xl, int xh, int ml, int mh, int al, int ah, int sl, int sh)
        {
            switch(cat)
            {
                case 'x': (xl, xh) = AdjustRange(op, v, xl, xh); break;
                case 'm': (ml, mh) = AdjustRange(op, v, ml, mh); break;
                case 'a': (al, ah) = AdjustRange(op, v, al, ah); break;
                case 's': (sl, sh) = AdjustRange(op, v, sl, sh); break;
                default: throw new Exception("double fuuu");
            }
            return (xl, xh, ml, mh, al, ah, sl, sh);
        }


        public override dynamic Solve_2()
        {
            var ans = 0L;
            var Q = new DLList<(string state, (int xl, int xh, int ml, int mh, int al, int ah, int sl, int sh) hilo)>
            {
                ("in", (1, 4000, 1, 4000, 1, 4000, 1, 4000))
            };
            while (Q.Count != 0)
            {
                var (wf, (xl, xh, ml, mh, al, ah, sl, sh)) = Q.Pop();
                if (xl > xh || ml > mh || al > ah || sl > sh) continue;
                if (wf == "A")
                {
                    ans += ((xh - xl + 1L) * (mh - ml + 1) * (ah - al + 1) * (sh - sl + 1));
                    continue;
                }
                if (wf == "R")
                    continue;
                foreach (var rule in workflows[wf])
                {
                    var result = rule;
                    var test = rule.Split(':');
                    if (test.Length == 2)
                    {
                        var cat = test[0][0];
                        var op = test[0][1];
                        result = test[1];
                        var v = int.Parse(test[0][2..]);
                        Q.Add((result, AdjustRanges(cat, "" + op, v, xl, xh, ml, mh, al, ah, sl, sh)));
                        (xl, xh, ml, mh, al, ah, sl, sh) = AdjustRanges(cat, op == '>' ? "<=" : ">=", v, xl, xh, ml, mh, al, ah, sl, sh);
                    }
                    else
                    {
                        Q.Add((result, (xl, xh, ml, mh, al, ah, sl, sh)));
                        break;
                    }
                }
            }
            return ans;
        }
    }
}
