using AoCUtil.Collections;
using RegExtract;

namespace AoC2016.Solutions
{
    internal class Day10() : BaseDay(2016)
    {
        Dictionary<int, (char type, int id)[]> Rules = [];
        DefaultDictionary<int, List<int>> Bots = new(true);
        DefaultDictionary<int, List<int>> Outputs = new(true);
        public override void ProcessInput()
        {
            Rules.Clear();
            Bots.Clear();
            Outputs.Clear();
            var groups = Input.GroupBy(l => l[0]);
            foreach (var line in groups.Where(g => g.Key == 'v').Single())
            {
                var(v, bot) = line.Extract<(int, int)>(@"value (\d+) goes to bot (\d+)");
                if (Bots[bot].Count == 2) throw new Exception("fuuuuuuuuuuuuu");
                Bots[bot].Add(v);
            }
            foreach (var line in groups.Where(g => g.Key == 'b').Single())
            {
                var (bot, lotype, lo, hitype, hi) = line.Extract<(int, char, int, char, int)>(@"bot (\d+) gives low to ([bo])\w+ (\d+) and high to ([bo])\w+ (\d+)");
                Rules[bot] = new[] { (lotype, lo), (hitype, hi) };
            }
        }

        int Solve(bool part1 = true)
        {
            var handsFull = new DLList<(int id, int[] chips)>
            {
                Bots.Where(p => p.Value.Count == 2).Select(kvp => (kvp.Key, kvp.Value.ToArray())).Single()
            };
            while (handsFull.Count > 0)
            {
                var (id, chips) = handsFull.PopLeft();

                int[] lohi = chips[0] > chips[1] ? [chips[1], chips[0]] : [chips[0], chips[1]];
                var rule = Rules[id];
                for (int i = 0; i < 2; i++)
                {
                    var (type, targetId) = rule[i];
                    var recepticle = (type == 'o' ? Outputs : Bots)[targetId];
                    recepticle.Add(lohi[i]);
                    if (type == 'b' && recepticle.Count == 2)
                    {
                        if (part1 && recepticle.Contains(17) && recepticle.Contains(61)) return targetId;
                        handsFull.AddLeft((targetId, recepticle.ToArray()));
                    }
                }
            }
            return Outputs[0].Single() * Outputs[1].Single() * Outputs[2].Single();
        }

        public override dynamic Solve_1() => Solve();

        public override dynamic Solve_2() => Solve(false);
    }
}
