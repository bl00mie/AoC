using RegExtract;
using System.Text;

namespace AoC2016.Solutions
{
    internal class Day11() : BaseDay(2016)
    {
        List<(List<string> chips, List<string> generators)> Floors = [];

        public override void ProcessInput()
        {
            Dictionary<string, string> substrs = [];
            foreach(var line in Input)
            {
                if (line.Contains("nothing relevant"))
                {
                    Floors.Add(([], []));
                    continue;
                }
                var items = line.Split("contains")[1].Replace(" and ", "")
                    .Extract<List<(string element, char type)>>(@"(a (\w\w)[\w-]+ (\w)[\w]+,? ?)+\.")
                    ?? [];
                
                List<string> chips = [];
                List<string> generators = [];
                
                foreach (var (element, type) in items)
                    if (type == 'm') chips.Add(element);
                    else generators.Add(element);
                Floors.Add((chips, generators));
            }
        }

        static string Statify(List<(List<string> chips, List<string> generators)> floors)
        {
            var sb = new StringBuilder();
            foreach (var floor in floors)
                sb.Append ($"({string.Join(",", floor.chips)}) , ({string.Join(",", floor.generators)})\n");
            return sb.ToString();
        }

        public override string Solve_1()
        {
            return "";
        }

        public override string Solve_2()
        {
            return "";
        }
    }
}
