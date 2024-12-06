using AoCUtil.Collections;

namespace AoC2019.Solutions
{
    public class Day06() : BaseDay(2019)
    {
        private readonly Dictionary<string,string> Map = [];
        public override void ProcessInput()
        {
            var orbits = Input.Extract<(string planet, string satellite)>(@"(\w+)\)(\w+)");
            foreach (var (planet, satellite) in orbits)
                Map[satellite] = planet;
        }

        public override dynamic Solve_1()
        {
            return Map.Keys.Select(GetChain).Sum(chain => chain.Count - 1);
        }

        public override dynamic Solve_2()
        {
            var you = GetChain("YOU");
            var san = GetChain("SAN");
            return you.Except(san).Count() + san.Except(you).Count() - 2;
        }

        List<string> GetChain(string satellite)
        {
            var chain = new List<string>() { satellite };
            while (Map.ContainsKey(satellite)) {
                satellite = Map[satellite];
                chain.Add(satellite);
            }
            return chain;
        }
    }
}
