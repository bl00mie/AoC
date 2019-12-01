using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2019._01
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Template

            var lines = AoCUtil.ReadLines("../../../input.txt");
            //var rx = new Regex(@"");

            var norms = from L in lines
                        //let rxr = rx.Match(L)
                        //select (rxr.Groups[""], rxr.Groups[""])
                        select (int.Parse(L))
                        ;

            #endregion Template



            #region Problem

            long tot = 0;
            foreach (var mass in norms)
            {
                long fuel = 0;
                long fuelfuel = mass;
                while (fuelfuel > 6)
                {
                    var val = fuelfuel / 3 - 2;
                    fuel += val;
                    fuelfuel = val;
                }
                tot += fuel;
            }
            Console.WriteLine(tot);

            #endregion
        }
    }
}
