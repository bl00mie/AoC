using System.Linq;
using RegExtract;

namespace AoC._2021._02
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2021, 02)
                .Extract<(char, int)>(@"(\w)\w* (\d+)").ToList();
            #endregion

            int x = 0, y = 0;
            int x2 = 0, y2 = 0, aim = 0;
            foreach (var (op, mag) in input)
            {
                switch(op)
                {
                    case 'f':
                        x += mag;
                        x2 += mag;
                        y2 += mag * aim;
                        break;
                    case 'd':
                        y += mag;
                        aim += mag;
                        break;
                    case 'u':
                        y -= mag;
                        aim -= mag;
                        break;
                    default:break;
                }
            }
            Ans(x * y);
            Ans(x2 * y2, 2);
        }
    }
}

