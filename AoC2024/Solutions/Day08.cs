using AoCUtil.Collections;

namespace AoC2024.Solutions
{
    public class Day08() : BaseDay(2024)
    {
        private Grid<char> Map = new();
        private Dictionary<char, List<Coord>> Frequencies = [];
        public override void ProcessInput()
        {
            Map = new Grid<char>(Input);
            Frequencies = Map.Where(pair => pair.v != '.')
                .GroupBy(pair => pair.v)
                .ToDictionary(x => x.Key, x => x.Select(i => i.p).ToList());
        }

        public override dynamic Solve_1()
        {
            var an = 0;
            foreach (var p in Map.Select(pair => pair.p))
            {
                foreach (var f in Frequencies)
                    foreach (var p1 in f.Value)
                        foreach (var p2 in f.Value)
                        {
                            if (p1 == p2) continue;
                            var d1 = Math.Abs(p.x - p1.x) + Math.Abs(p.y - p1.y);
                            var d2 = Math.Abs(p.x - p2.x) + Math.Abs(p.y - p2.y);

                            var dx1 = p.x - p1.x;
                            var dx2 = p.x - p2.x;
                            var dy1 = p.y - p1.y;
                            var dy2 = p.y - p2.y;

                            if ((d1 == 2 * d2 || d2 == 2 * d1 ) && dx1 * dy2 == dx2 * dy1)
                            {
                                an++;
                                goto Found;
                            }    
                        }
                Found:;
            }
            return an;
        }

        public override dynamic Solve_2()
        {
            var an = 0;
            foreach (var p in Map.Select(pair => pair.p))
            {
                foreach (var f in Frequencies)
                    foreach (var p1 in f.Value)
                        foreach (var p2 in f.Value)
                        {
                            if (p1 == p2) continue;

                            var dx1 = p.x - p1.x;
                            var dx2 = p.x - p2.x;
                            var dy1 = p.y - p1.y;
                            var dy2 = p.y - p2.y;

                            if (dx1 * dy2 == dx2 * dy1)
                            {
                                an++;
                                goto Found;
                            }
                        }
                    Found:;
            }
            return an;
        }
    }
}
