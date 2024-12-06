using RegExtract;

namespace AoC2016.Solutions
{
    internal class Day08() : BaseDay(2016)
    {
        string[] Sample = ["rect 3x2", "rotate column x=1 by 1", "rotate row y=0 by 4", "rotate column x=1 by 1"];


        const int H = 6;
        const int W = 50;
        readonly char[][] Screen = new char[H][];
        (string cmd, string @params)[] Instructions = [];

        public override void ProcessInput()
        {
            foreach (var i in Enumerable.Range(0, H))
                Screen[i] = "..................................................".ToCharArray();
            Instructions = Input.Extract<(string, string)>(@"(\w+) (.*)").ToArray();
        }

        void go()
        {
            foreach (var (cmd, @params) in Instructions)
            {
                if (cmd == "rect")
                {
                    var (X, Y) = @params.Extract<(int, int)>(@"(\d+)x(\d+)");
                    foreach (var y in Enumerable.Range(0, Y))
                        foreach (var x in Enumerable.Range(0, X))
                            Screen[y][x] = '#';
                }
                else if (cmd == "rotate")
                {
                    var (type, i, n) = @params.Extract<(char, int, int)>(@"([rc])[a-z ]+=(\d+) by (\d+)");
                    if (type == 'r')
                    {
                        var s = string.Join("", Screen[i]);
                        s = s[^n..] + s[..^n];
                        foreach (var j in Enumerable.Range(0, W))
                            Screen[i][j] = s[j];
                    }
                    if (type == 'c')
                    {
                        var s = string.Join("", Enumerable.Range(0, H).Select(j => Screen[j][i]));
                        var lhs = s[^n..];
                        var rhs = s[..^n];
                        s = s[^n..] + s[..^n];
                        foreach (var j in Enumerable.Range(0, H))
                            Screen[j][i] = s[j];
                    }
                }
            }
        }

        void Draw()
        {
            foreach (var row in Screen) Console.WriteLine(string.Join("", row));
        }

        public override dynamic Solve_1()
        {
            go();
            return Screen.Sum(row => row.Count(c => c == '#'));
        }

        public override dynamic Solve_2()
        {
            go();
            Draw();
            return "read the console";
        }
    }
}
