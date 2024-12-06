using AoCUtil.Collections;

namespace AoC2024.Solutions
{
    public class Day05() : BaseDay(2024)
    {
        private readonly Dictionary<int, HashSet<int>> RulesBefore = [];
        private readonly Dictionary<int, HashSet<int>> RulesAfter = [];
        List<HashSet<int>> Updates = [];
        public override void ProcessInput()
        {
            var input = AoC.AoCUtil.GroupInput(Input);
            foreach (var (l, r) in input.First().Extract<(int l, int r)>(@"(\d+)\|(\d+)"))
            {
                if (!RulesBefore.ContainsKey(l)) RulesBefore[l] = [];
                RulesBefore[l].Add(r);
                if (!RulesAfter.ContainsKey(r)) RulesAfter[r] = [];
                RulesAfter[r].Add(l);
            }
            Updates = input.Last().Extract<HashSet<int>>(@"((\d+),?)+").ToList();
        }

        public override dynamic Solve_1()
        {
            var middle = 0;
            foreach (var update in Updates)
            {
                var valid = true;
                foreach (var x in update.Select((v,i) => (v, i)))
                    foreach (var y in update.Select((v,i) => (v,i)))
                        if (x.i < y.i && RulesAfter[x.v].Contains(y.v))
                            valid = false;
                if (valid)
                    middle += update.ElementAt(update.Count / 2);
            }
            return middle;
        }

        public override dynamic Solve_2()
        {
            var middle = 0;
            foreach (var update in Updates)
            {
                var valid = true;
                foreach (var x in update.Select((v, i) => (v, i)))
                    foreach (var y in update.Select((v, i) => (v, i)))
                        if (x.i < y.i && RulesAfter[x.v].Contains(y.v))
                            valid = false;
                if (!valid)
                {
                    var good = new List<int>();
                    var noneBefore = new DLList<int>();
                    var beforeCount = new Dictionary<int, int>();
                    foreach (var page in update)
                    {
                        beforeCount[page] = RulesBefore[page].Where(v => update.Contains(v)).Count();
                        if (beforeCount[page] == 0)
                            noneBefore.Add(page);
                    }
                    while (noneBefore.Count != 0)
                    {
                        var x = noneBefore.PopLeft();
                        good.Add(x);
                        foreach (var y in RulesAfter[x])
                        {
                            if (beforeCount.TryGetValue(y, out int v))
                            {
                                beforeCount[y] = --v;
                                if (v == 0)
                                    noneBefore.Add(y);
                            }
                        }
                    }
                    middle += good[good.Count / 2];
                }
            }
            return middle;
        }
    }
}
