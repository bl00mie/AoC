using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public static class NumericExtensions
    {
        public static T Clamp<T>(this T value, T min, T max) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => (value.CompareTo(min) <= 0)? min : (value.CompareTo(max) >= 0) ? max : value;

        public static IEnumerable<long> Factors(this long value)
        {
            var max = value;
            for (long i=2; i<max; i++)
                if (value % i == 0)
                {
                    yield return i;
                    yield return (max = value/i);
                }
        }

        public static ulong LCM(this IEnumerable<ulong> values)
            => values.Aggregate((ulong)1L, (a, b) => (a * b) / AoC.AoCUtil.GCD(a, b));
    }
}
