using System;
using System.Linq;

namespace AoC._2015._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = AoCUtil.GetAocInput(2015, 2);
            var input = lines.Select(x =>
            {
                return x.Split('x').Select(int.Parse).OrderBy(x => x).ToArray();
            }).ToArray();

            int totPaper = 0;
            int totRibbon = 0;
            foreach (var d in input)
            {
                totRibbon += 2 * (d[0] + d[1]) + d[0] * d[1] * d[2];
                totPaper += d[0]*d[1]*3 + (d[0]*d[2] + d[1]*d[2]) * 2;
            }
            Console.WriteLine(totPaper);
            Console.WriteLine(totRibbon);
        }
    }
}
