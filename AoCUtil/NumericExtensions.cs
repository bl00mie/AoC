using System;

namespace AoC
{
    public static class NumericExtensions
    {
        public static T Clamp<T>(this T value, T min, T max) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            return (value.CompareTo(min) <= 0)? min : (value.CompareTo(max) >= 0) ? max : value;
        }
    }
}
