using System;
using System.Collections.Generic;
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


        public static IEnumerable<string> getAocInput(int year, int day)
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
                    client.Headers.Add(HttpRequestHeader.Cookie, "session=53616c7465645f5fd766bf5e9d9f033ef0f7c8aa656e2ef5063f45bb08566b8840fc11e5666dd297e585d62a10c139c6");
                    string inputUrl = string.Format("https://adventofcode.com/{0}/day/{1}/input", year, day);

                    webStream = client.OpenRead(inputUrl);
                    sr = new StreamReader(webStream);
                    var lines = ReadLines(sr).ToArray<string>();

                    sw = new StreamWriter(filepath);
                    foreach (string line in lines)
                    {
                        sw.WriteLineAsync(line);
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
    }
}
