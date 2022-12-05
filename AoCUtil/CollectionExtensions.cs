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
    }
}
