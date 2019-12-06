using System;
using System.Security.Cryptography;
using System.Text;

namespace AoC._2015._04
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "ckczppom";

            #region Part 1

            long l = 0;
            MD5 md5 = MD5.Create();
            while (true)
            {
                byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input + l));
                if (hash[0] == 0 && hash[1] == 0 && hash[2]>>4 == 0)
                {
                    Console.WriteLine(l);
                    break;
                }
            }

            #endregion Part 1

            #region Part 2
            // starting at previous l is fine, since it was the first 5-zero value
            while (true)
            {
                byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input + l));
                if (hash[0] == 0 && hash[1] == 0 && hash[2] == 0)
                {
                    Console.WriteLine(l);
                    break;
                }
            }

            #endregion
        }
    }
}
