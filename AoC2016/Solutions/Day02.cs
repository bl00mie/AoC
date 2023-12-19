using AoC;
using System.Text;

namespace AoC2016.Solutions
{
    internal class Day02 : BaseDay2016
    {
        string GetCode(Dictionary<(int x, int y), char> pad, (int x, int y) pos)
        {
            var ans = new StringBuilder();
            foreach (var line in Input)
            {
                foreach (var dc in line)
                {
                    var dir = GridVector.Lookup[$"{dc}"];
                    var n = pos + dir;
                    if (pad.ContainsKey(n)) pos = n;
                }
                ans.Append(pad[pos]);
            }
            return ans.ToString();
        }
        public override dynamic Solve_1()
        {
            var pad = new Dictionary<(int x, int y), char>()
            {
                [(0, 0)] = '1', [(1, 0)] = '2', [(2, 0)] = '3',
                [(0, 1)] = '4', [(1, 1)] = '5', [(2, 1)] = '6',
                [(0, 2)] = '7', [(1, 2)] = '8', [(2, 2)] = '9'
            };
            return GetCode(pad, (1, 1));
        }

        public override dynamic Solve_2()
        {
            var pad = new Dictionary<(int x, int y), char>()
            {
                                                [(2, 0)] = '1',
                                [(1, 1)] = '2', [(2, 1)] = '3', [(3, 1)] = '4',
                [(0, 2)] = '5', [(1, 2)] = '6', [(2, 2)] = '7', [(3, 2)] = '8', [(4, 2)] = '9',
                                [(1, 3)] = 'A', [(2, 3)] = 'B', [(3, 3)] = 'C',
                                                [(2, 4)] = 'D'
            };
            return GetCode(pad, (0, 2));
        }
    }
}
