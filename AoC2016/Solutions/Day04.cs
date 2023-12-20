using RegExtract;
using System.Security.Cryptography;

namespace AoC2016.Solutions
{
    internal class Day04 : BaseDay2016
    {
        List<(string words, int id, string chk)> rooms = [];
        public override void ProcessInput()
        {
            rooms = Input.Extract<(string words, int id, string chk)>(@"([a-z-]+)-(\d+)\[([a-z]+)\]").ToList();
        }

        public override dynamic Solve_1()
        {
            rooms = rooms.Where(room => room.words.Replace("-", "")
                    .GroupBy(c => c)
                    .Select(group => (group.Key, group.Count()))
                    .OrderBy(x => x.Key)
                    .OrderByDescending(x => x.Item2)
                    .Take(5)
                    .Select(x => x.Key)
                    .ToArray()
                    .Zip(room.chk)
                    .All(pair => pair.First == pair.Second))
                .ToList();
            
            return rooms.Sum(room => room.id);
        }

        static string Decrypt((string words, int id, string chk) room)
            => new(room.words.Select(c =>
            {
                if (c == '-') return ' ';
                return (char)((c - 'a' + room.id) % 26 + 'a');
            }).ToArray());

        public override dynamic Solve_2()
            => rooms.Where(room => Decrypt(room) == "northpole object storage").Single().id;
    }
}
