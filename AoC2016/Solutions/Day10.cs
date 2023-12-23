using AoCUtil.Collections;
using RegExtract;

namespace AoC2016.Solutions.Solutions
{
    internal class Day10() : BaseDay(2016)
    {
        Dictionary<int, (char lotype, int lo, char hitype, int hi)> Rules = [];
        DefaultDictionary<int, List<int>> Bots = new(true);
        DefaultDictionary<int, List<int>> Outputs = new(true);
        public override void ProcessInput()
        {
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
                Rules[bot] = (lotype, lo, hitype, hi);
            }
        }

        public override dynamic Solve_1()
        {
            var handsFull = Bots.Where(p => p.Value.Count == 2);
            while (handsFull.Count() > 0)
            {

            }
            return "";
        }

        public override dynamic Solve_2()
        {
            return "";
        }
    }
}
