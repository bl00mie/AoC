using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2015._01
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Input

            var input = AoCUtil.GetAocInput(2015,1).First<string>();

            #endregion Input

            #region Problem

            int floor = 0;
            int firstBasement = -1;

            for (int i = 0; i< input.Length; i++)
            {
                if (input[i] == ')') floor--;
                else floor++;
                if (floor < 0 && firstBasement == -1)
                {
                    firstBasement = i+1;
                }
            }
            Debug.WriteLine(floor);
            Debug.WriteLine(firstBasement);

            #endregion Problem
        }
    }
}
