using System.Security.Cryptography;
using System.Text;
using AoCUtil.Collections;

namespace AoC2023.Solutions
{
    internal class Day14() : BaseDay(2023)
    {
        char[][] Map = [];

        public override void ProcessInput()
        {
            Map = Input.Select(l => l.ToCharArray()).ToArray();
        }

        static int Tally(char[][] map) => map.Select((line, y) => (line, y))
                .Sum((p) => p.line
                    .Where(c => c == 'O')
                    .Count() * (map.Length - p.y));

        static void Roll(char[][] map, GridVector dir, bool ul = true)
        {
            for (var y = ul ? 0 : map.Length - 1; y != (ul ? map.Length : -1); y += ul ? 1 : -1)
                for (var x = ul ? 0 : map[0].Length - 1; x != (ul ? map[0].Length : -1); x += ul ? 1 : -1)
                    if (map[y][x] == 'O')
                    {
                        var p = new Coord(x, y) + dir;
                        while (p.x >= 0 && p.y >= 0 && p.x < map.Length && p.y < map[0].Length && map[p.y][p.x] == '.')
                        {
                            map[p.y][p.x] = 'O';
                            map[p.y - dir.dy][p.x - dir.dx] = '.';
                            p += dir;
                        }
                    }
        }

        static readonly IEnumerable<(GridVector dir, bool upperLeft)> cycleRolls = [(GridVector.S, true), (GridVector.W, true), (GridVector.N, false), (GridVector.E, false)];
        static void DoCycle(char[][] map)
        {
            foreach (var (dir, upperLeft) in cycleRolls)
                Roll(map, dir, upperLeft);
        }

        static void Draw(char[][] map)
        {
            var sb = new StringBuilder(new string('-', 20));
            sb.AppendLine();
            foreach (var line in map)
                sb.AppendLine(string.Join("", line));
            Console.WriteLine(sb);
        }

        public override dynamic Solve_1()
        {
            var map = Map.ToArray();
            Roll(map, GridVector.S);
            return Tally(map);
        }

        public override dynamic Solve_2()
        {
            var map = Map.ToArray();
            var sha256 = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
            var hashes = new Dictionary<string, long>();
            for (var cycle = 0L; cycle < 1000000000L; cycle++)
            {
                DoCycle(map);
                foreach (var line in map)
                    sha256.AppendData(Encoding.UTF8.GetBytes(line));
                var hash = BitConverter.ToString(sha256.GetHashAndReset());
                if (hashes.TryGetValue(hash, out long value))
                {
                    var delta = cycle - value;
                    var skips = (1000000000L - cycle) / delta;
                    cycle += skips * delta + 1;
                    while (cycle++ < 1000000000L)
                        DoCycle(map);
                }
                else hashes[hash] = cycle;
            }
            return Tally(map);
        }
    }
}
