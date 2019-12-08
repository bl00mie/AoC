﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;

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
			return ReadLines(new StreamReader(fileName));
		}


        public static IEnumerable<string> GetAocInput(int year, int day, string sessionCookie = "53616c7465645f5fd766bf5e9d9f033ef0f7c8aa656e2ef5063f45bb08566b8840fc11e5666dd297e585d62a10c139c6")
        {
            string filepath = string.Format("../../../../../inputs/{0}_{1}", year, day);
            if (!File.Exists(filepath))
            {
                Stream webStream = null;
                StreamReader sr = null;
                StreamWriter sw = null;
                try
                {
                    var client = new WebClient();

                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    client.Headers.Add(HttpRequestHeader.Cookie, string.Format("session={0}", sessionCookie));
                    string inputUrl = string.Format("https://adventofcode.com/{0}/day/{1}/input", year, day);

                    webStream = client.OpenRead(inputUrl);
                    sr = new StreamReader(webStream);
                    var lines = ReadLines(sr).ToArray<string>();

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
                        sr.Close();
                    }
                    if (sw != null)
                    {
                        sw.Close();
                    }
                    if (webStream != null)
                    {
                        webStream.Close();
                    }
                }
            }
            return ReadLines(filepath);
        }

        public static string StringifyArray(Array a)
        {
            string s = "[ ";
            foreach (var item in a)
            {
                s += item.ToString() + " ";
            }
            return s + " ]";
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
                    if (stack.Count() == 0) yield break;
                    else if (curitems.Count() == 0) yield return acc;

                    (curitems, pos, acc) = stack.Peek();
                    pos = pos + 1;
                    stack = stack.Pop();
                }
                else
                {
                    stack = stack.Push((curitems, pos, acc));
                    (curitems, pos, acc) = (curitems.RemoveAt(pos), 0, acc.Add(curitems[pos]));
                }
            }
        }
    }
}