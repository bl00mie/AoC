using System.Linq;

namespace AoC._2022._1
{
    class Program : ProgramBase
    {
        static void Main()
        {
            var elves = AoCUtil.GroupInput(2022, 1)
                .Select(g => g.Sum(int.Parse))
                .OrderByDescending(s => s)
                .ToArray();
            Ans(elves[0]);
            Ans2(elves[..3].Sum());
        }
    }
}

