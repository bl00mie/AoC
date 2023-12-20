using AoC;
using RegExtract;

namespace AoC2016.Solutions.Solutions
{
    internal class Day07 : BaseDay2016
    {
        readonly string[] Sample =
        [
            "abba[mnop]qrst",
            "abcd[bddb]xyyx",
            "aaaa[qwer]tyui",
            "ioxxoj[asdfgh]zxcvbn"
        ];

        List<(List<string>super, List<string>hyper)> ips = [];
        public override void ProcessInput()
        {
            ips = [];
            foreach (var parts in Input.Select(l => l.Split("[]".ToCharArray())))
            {
                List<string> hyper = [];
                List<string> super = [];
                for (int i=0; i<parts.Length; i++)
                    if (i % 2 == 0) super.Add(parts[i]);
                    else hyper.Add(parts[i]);
                ips.Add((super, hyper));
            }
        }

        static bool ABBA(string s)
        {
            for (var i = 0; i < s.Length - 3; i++)
                if ((s[i] == s[i + 3]) && (s[i+1] == s[i+2]) && (s[i] != s[i + 1]))
                    return true;
            return false;
        }

        static IEnumerable<string> ABAs(IEnumerable<string> ss)
        {
            foreach (var s in ss)
                for (var i = 0; i < s.Length-2; i++)
                    if (s[i] == s[i + 2] && s[i] != s[i + 1])
                        yield return new string(s.AsSpan()[i..(i+3)]);
        }

        public override dynamic Solve_1()
        {
            return ips.Count(ip => !ip.hyper.Any(ABBA) && ip.super.Any(ABBA));
        }

        public override dynamic Solve_2()
        {
            return ips.Count(ip =>
            {
                var abas = ABAs(ip.super).ToArray();
                foreach (var aba in ABAs(ip.super))
                    foreach (var hyper in ip.hyper)
                        if (hyper.Contains($"{aba[1]}{aba[0]}{aba[1]}")) return true;
                return false;
            });
        }
    }
}
