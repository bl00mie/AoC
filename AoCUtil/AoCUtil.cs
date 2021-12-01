using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    public static class AoCUtil
    {
		public static IEnumerable<string> ReadLines(StreamReader streamReader)
		{
			string line;
			while ((line = streamReader.ReadLine()) != null)
				yield return line;
		}

		public static IEnumerable<string> ReadLines(string fileName)
		{
            using (var sr = new StreamReader(fileName))
            {
                return ReadLines(sr);
            }
		}

        public static IEnumerable<string> GetAocInput(int year, int day, string sessionCookie = null)
        {
            return GetAocInputAsync(year, day, sessionCookie).Result;
        }

        public static async Task<IEnumerable<string>> GetAocInputAsync(int year, int day, string sessionCookie=null)
        {
            if (sessionCookie == null)
            {
                sessionCookie = ReadLines("../../session_cookie").First();
            }

            string filepath = $"../../inputs/{year}_{day}";
            if (!File.Exists(filepath))
            {
                var baseAddress = new Uri($"https://adventofcode.com/{year}/day/{day}/input");
                var cookieContainer = new CookieContainer();
                using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
                using var client = new HttpClient(handler) { BaseAddress = baseAddress };
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                cookieContainer.Add(baseAddress, new Cookie("session", sessionCookie));
                var response = await client.GetAsync(baseAddress);
                if (response.IsSuccessStatusCode)
                {
                    using var writer = new StreamWriter(filepath);
                    writer.Write(response.Content);
                    writer.Flush();
                }
            }
            return ReadLines(filepath);
        }

        public static string StringifyArray(Array a)
        {
            StringBuilder sb = new StringBuilder(a.Length * 20);
            sb.Append("[ ");
            foreach (var item in a)
                sb.AppendFormat("{0} ", item);
            sb.Append(" ]");
            return sb.ToString();
        }

        public static IEnumerable<IEnumerable<T>> GetPermutationsRecursive<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutationsRecursive(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }


        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list)
        {
            var items = ImmutableList.CreateRange(list);
            var stack = ImmutableStack<(ImmutableList<T> cur, int pos, ImmutableList<T> acc)>.Empty;

            var (curitems, pos, acc) = (items, 0, ImmutableList<T>.Empty);

            while (true)
            {
                if (pos >= curitems.Count())
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
        }

        public static IEnumerable<IList<T>> GetVariations<T>(IList<T> offers, int length)
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
                {
                    if (startIndices[i] >= offers.Count)
                    {
                        startIndices[i] = 0;
                        startIndices[i - 1]++;
                    }
                    else
                        break;
                }
                variationElements.Clear();
            }
        }

        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy((item) => rnd.Next());
        }


        public static ulong GCD(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        public static ulong LCM(ulong a, ulong b)
        {
            return a / GCD(a, b) * b;
        }
    }
}
