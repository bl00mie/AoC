namespace AoC2019.Solutions
{
    public class Day08() : BaseDay(2019)
    {
        private List<string> Layers;
        private readonly int H = 6;
        private readonly int W = 25;
        public override void ProcessInput()
        {
            Layers = Input.First().Chunk(H * W).Select(l => new string(l)).ToList();
        }

        public override dynamic Solve_1()
        {
            var mostZeroes = Layers.Select(layer => (layer.Where(c => c == '0').Count(), layer)).OrderBy(pair => pair.Item1).First();
            return mostZeroes.layer.Where(c => c == '1').Count() * mostZeroes.layer.Where(c => c == '2').Count();
        }

        public override dynamic Solve_2()
        {
            var pixeled = Layers.Select(l => l.Chunk(W).Select(chars => new string(chars)).ToArray()).ToList();

            var output = "";
            for(var y = 0; y< H; y++)
            {
                for(var x = 0;x < W; x++)
                {
                    var layer = 0;
                    while (pixeled[layer][y][x] == '2') layer++;
                    output += pixeled[layer][y][x] == '0' ? ' ' : '█';
                }
                output += "\n";
            }

            return output;
        }
    }
}
