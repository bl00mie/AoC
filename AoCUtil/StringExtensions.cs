using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public static class StringExtensions
    {
        public static IEnumerable<int> GetInts(this string value)
        {
            return value.Split(',').Select(int.Parse);
        }
    }
}
