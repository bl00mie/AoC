using RegExtract;

namespace AoC2016.Solutions.Solutions
{
    internal class Day09() : BaseDay(2016)
    {
        string data = string.Empty;
        public override void ProcessInput()
        {
            data =
                Input.Single();
                //"(27x12)(20x12)(13x14)(7x10)(1x12)A"; //241920
                //"(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN" //445
        }

        long Decompress(string s, bool v2 = false)
        {
            long ans = 0;
            var p = 0;
            while (p < s.Length)
                if (s[p++] == '(')
                {
                    var p2 = s.IndexOf(')', p+1) + 1;
                    if (p2 == -1)
                        throw new Exception("fuuuuu");
                    var (sz, n) = s[p..p2].Extract<(int, int)>(@"(\d+)x(\d+)\)");
                    p = p2;
                    if (v2)
                        ans += (n * Decompress(s[p..(p + sz)], true));
                    else
                        ans += sz * n;
                    p += sz;
                }
                else
                    ans++;
            return ans;
        }

        public override dynamic Solve_1() => Decompress(data);

        public override dynamic Solve_2() => Decompress(data, true);
    }
}
