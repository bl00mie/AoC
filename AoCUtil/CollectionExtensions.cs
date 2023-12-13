using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AoC
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> Pop<T>(this Stack<T> stack, int count)
        {
            while(count-- > 0) { }
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

        public static IEnumerable<(T a, T b)> SlidingWindow<T>(this IEnumerable<T> input) => input.Skip(1).Zip(input);

        public static IEnumerable<IEnumerable<T>> GetPermutationsRecursive<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutationsRecursive(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }


        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list)
        {
            var items = ImmutableList.CreateRange(list);
            var stack = ImmutableStack<(ImmutableList<T> cur, int pos, ImmutableList<T> acc)>.Empty;

            var (curitems, pos, acc) = (items, 0, ImmutableList<T>.Empty);

            while (true)
                if (pos >= curitems.Count)
                {
                    if (!stack.Any()) yield break;
                    if (!curitems.Any()) yield return acc;

                    (curitems, pos, acc) = stack.Peek();
                    pos += 1;
                    stack = stack.Pop();
                }
                else
                {
                    stack = stack.Push((curitems, pos, acc));
                    (curitems, pos, acc) = (curitems.RemoveAt(pos), 0, acc.Add(curitems[pos]));
                }
        }

        public static IEnumerable<IList<T>> GetVariations<T>(this IList<T> offers, int length)
        {
            var startIndices = new int[length];
            var variationElements = new HashSet<T>();

            while (startIndices[0] < offers.Count)
            {
                var variation = new List<T>(length);
                var valid = true;
                for (int i = 0; i < length; ++i)
                {
                    var element = offers[startIndices[i]];
                    if (variationElements.Contains(element))
                    {
                        valid = false;
                        break;
                    }
                    variation.Add(element);
                    variationElements.Add(element);
                }
                if (valid)
                    yield return variation;

                startIndices[length - 1]++;
                for (int i = length - 1; i > 0; --i)
                    if (startIndices[i] >= offers.Count)
                    {
                        startIndices[i] = 0;
                        startIndices[i - 1]++;
                    }
                    else
                        break;
                variationElements.Clear();
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var rnd = new Random();
            return source.OrderBy((item) => rnd.Next());
        }

        public static IEnumerable<TResult> ForAllPairs<TItem, TResult>(this IEnumerable<TItem> items, Func<TItem, TItem, TResult> operation)
        {
            foreach(var (itemA, i) in items.Select((item, index) => (item, index)))
                foreach(var itemB in items.Skip(i+1))
                    yield return operation(itemA, itemB);
        }
    }
}
