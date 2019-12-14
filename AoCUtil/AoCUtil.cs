using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

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
            var sr = new StreamReader(fileName);
            try
            {
                return ReadLines(sr);
            }
            finally
            {
                //sr.Dispose();
            }
		}


        public static IEnumerable<string> GetAocInput(int year, int day, string sessionCookie=null)
        {
            if (sessionCookie == null)
            {
                sessionCookie = ReadLines("../../session_cookie").First();
            }

            string filepath = string.Format("../../inputs/{0}_{1}", year, day);
            if (!File.Exists(filepath))
            {
                Stream webStream = null;
                StreamReader sr = null;
                StreamWriter sw = null;
                WebClient client = null;
                try
                {
                    client = new WebClient();

                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    client.Headers.Add(HttpRequestHeader.Cookie, string.Format("session={0}", sessionCookie));
                    string inputUrl = string.Format("https://adventofcode.com/{0}/day/{1}/input", year, day);

                    webStream = client.OpenRead(inputUrl);
                    sr = new StreamReader(webStream);
                    var lines = ReadLines(sr).ToArray();

                    sw = new StreamWriter(filepath);
                    foreach (string line in lines)
                    {
                        sw.WriteLine(line);
                    }
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Dispose();
                    }
                    if (sw != null)
                    {
                        sw.Dispose();
                    }
                    if (webStream != null)
                    {
                        webStream.Dispose();
                    }
                    if (client != null)
                    {
                        client.Dispose();
                    }
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
