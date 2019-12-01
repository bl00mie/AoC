using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2019._02
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

            Console.WriteLine("asdf");

            #endregion
        }
    }
}
