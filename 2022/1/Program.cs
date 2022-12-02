using System.Linq;

namespace AoC._2022._1
{
    class Program : ProgramBase
    {
        static void Main()
        {
            var elves = AoCUtil.GroupInput(2022, 1).Select(g => g.Sum(l => int.Parse(l)));
            var sorted = elves.OrderByDescending(x => x).ToList();
            Ans(sorted[0]);
            Ans2(sorted[0]+sorted[1]+sorted[2]);
        }
    }
}

