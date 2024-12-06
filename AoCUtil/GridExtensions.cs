using AoCUtil.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoCUtil
{
    static class GridExtensions
    {
        public static IEnumerable<Coord> GetPoints(this Coord p, GridVector v)
        {
            if (v.dx != 0 && v.dy != 0) throw new Exception("GetPoints requires horizontal or vertical vectors");
            var n = p;
            foreach (var _ in Enumerable.Range(0, v.dx + v.dy))
                yield return (n += v);
        }
    }
}