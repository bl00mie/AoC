using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace AoC._2015._03
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> DX = new Dictionary<char, int>() { { '^', 0 }, { 'v', 0 }, { '<', -1 }, { '>', 1 } };
            Dictionary<char, int> DY = new Dictionary<char, int>() { { '^', 1 }, { 'v', -1 }, { '<', 0 }, { '>', 0 } };

            var input = AoCUtil.GetAocInput(2015, 3).First<string>();

            #region Part 1

            HashSet<Tuple<int, int>> houses = new HashSet<Tuple<int, int>>();
            houses.Add(Tuple.Create(0, 0));

            int x = 0, y = 0;
            int rx = 0, ry = 0;
            foreach (char c in input)
            {
                x += DX[c]; y += DY[c];
                houses.Add(new Tuple<int, int>(x, y));
            }

            Console.WriteLine(houses.Count);

            #endregion Part 1

            #region Part 2


            houses.Clear();
            houses.Add(Tuple.Create(0, 0));
            x = 0; y = 0;
            rx = 0; ry = 0;
            for (int i = 0; i < input.Length; i++)
            {
                x += DX[input[i]]; y += DY[input[i++]];
                houses.Add(new Tuple<int, int>(x, y));
                rx += DX[input[i]]; ry += DY[input[i]];
                houses.Add(new Tuple<int, int>(rx, ry));
            }

            Console.WriteLine(houses.Count);

            #endregion
        }
    }
}
