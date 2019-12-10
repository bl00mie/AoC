using System.Collections.Generic;
using System.Linq;

namespace AoC._2019._08
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input

            int X = 25, Y = 6, A = X*Y;
            var input = AoCUtil.GetAocInput(2019, 08).First();

            #endregion

            #region Part 1

            List<int[,]> layers = new List<int[,]>();
            int min = int.MaxValue;
            int OT = 0;
            for (int p=0; p < input.Length; p+=A)
            {
                var s = input.Substring(p, A);
                var layer = new int[X, Y]; layers.Add(layer);
                int[] vc = new int[3];
                for (int i = 0; i < A; i++)
                {
                    int v = s[i] - '0';
                    layer[i % X, i / X] = v;
                    vc[v]++;
                }
                if (vc[0] < min)
                {
                    min = vc[0];
                    OT = vc[1] * vc[2];
                }
            }
            Ans(OT);

            #endregion Part 1

            Ans("", 2);
            #region Part 2
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                    foreach (var layer in layers)
                        if (layer[x, y] != 2)
                        {
                            if (layer[x, y]==1)
                                Write('*');
                            else
                                Write(' ');
                            break;
                        }
                WL();
            }

            #endregion
        }
    }
}