using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public static class NumericExtensions
    {
        public static T Clamp<T>(this T value, T min, T max) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            return (value.CompareTo(min) <= 0)? min : (value.CompareTo(max) >= 0) ? max : value;
        }

        public static List<int> Factors(this int value)
        {
            var factors = new HashSet<int>();
            factors.Add(1); factors.Add(value);
            int max = value;
            for (int i=2; i<max; i++)
                if (value % i == 0)
                {
                    int top = value / i;
                    max = top;
                    factors.Add(i);
                    if (top == i) return factors.ToList();
                    factors.Add(top);
                }

            return factors.ToList();
        }

        public static ulong LCM(this IEnumerable<ulong> values)
            => values.Aggregate((ulong)1L, (a, b) => (a * b) / AoC.AoCUtil.GCD(a, b));
    }
}
