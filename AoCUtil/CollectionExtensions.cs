using AoC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoCUtil
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> Pop<T>(this Stack<T> stack, int count)
        {
            foreach (var _ in Enumerable.Range(0, count))
                yield return stack.Pop();
        }

        public static void Push<T>(this Stack<T> stack, IEnumerable<T> items)
        {
            foreach (var item in items)
                stack.Push(item);
        }

        public static (int xLo, int xHi, int yLo, int yHi) GetRectangle(this IEnumerable<Coord> grid)
        {
            var X = int.MaxValue;
            var XX = int.MinValue;
            var Y = int.MaxValue;
            var YY = int.MinValue;

            foreach (var p in grid)
            {
                X = Math.Min(X, p.x);
                XX = Math.Max(XX, p.x);
                Y = Math.Min(Y, p.y);
                YY = Math.Max(YY, p.y);
            }
            return (X, XX, Y, YY);
        }
    }
}
