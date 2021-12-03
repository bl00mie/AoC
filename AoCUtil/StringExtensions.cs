﻿using System;
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
        public static IEnumerable<long> GetLongs(this string value)
        {
            return value.Split(',').Select(long.Parse);
        }
        public static IEnumerable<double> GetDoubles(this string value)
        {
            return value.Split(',').Select(double.Parse);
        }

        public static int Int(this string value, int radix = 10)
        {
            return Convert.ToInt32(value, radix);
        }
    }
}
