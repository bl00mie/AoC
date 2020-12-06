using System.Linq;

namespace AoC._2020._01
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2020, 01).Select(s => long.Parse(s)).ToList<long>();
            #endregion

            bool done1 = false, done2 = false;
            foreach (var x in input)
                foreach (var y in input)
                    if (x + y == 2020 && !done1)
                    {
                        done1 = true;
                        Ans(x * y);
                    }
                    else if (x + y < 2020)
                        foreach (var z in input)
                            if (done2) break;
                            else if (x + y + z == 2020)
                            {
                                done2 = true;
                                Ans2(x * y * z);
                            }
        }
                                
    }
}

