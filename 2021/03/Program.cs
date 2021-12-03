using System.Linq;
using AoC.Sub;

namespace AoC._2021._03
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2021, 03).ToArray();
            #endregion

            #region Part 1
            Ans(Diagnostics.PowerConsumption(input));
            #endregion Part 1

            #region Part 2
            Ans(Diagnostics.LifeSupportRating(input));
            #endregion
        }
    }
}