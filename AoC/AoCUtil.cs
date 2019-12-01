using System.Collections.Generic;
using System.IO;

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
	}
}
