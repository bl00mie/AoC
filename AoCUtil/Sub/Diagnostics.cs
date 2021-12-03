using System.Collections.Generic;
using System.Linq;

namespace AoC.Sub
{
    public static class Diagnostics
    {
        public static (long gamma, long epsilon) GammaEpsilon(IEnumerable<string> diagnosticReport)
        {
            var gamma = 0L;
            var epsilon = 0L;
            var mul = 1;
            for (int x = diagnosticReport.First().Length - 1; x >= 0; x--)
            {
                if (Ones(diagnosticReport, x).Count() > diagnosticReport.Count() / 2.0)
                    gamma += mul;
                else
                    epsilon += mul;
                mul *= 2;
            }
            return (gamma, epsilon);
        }

        public static long PowerConsumption(IEnumerable<string> diagnosticReport)
        {
            var (gamma, epsilon) = GammaEpsilon(diagnosticReport);
            return gamma * epsilon;
        }

        public static (long o2, long co2) PressRatings(IEnumerable<string> diagnosticReport)
        {
            IEnumerable<string> o2 = diagnosticReport.ToArray(), co2 = diagnosticReport.ToArray();
            for (int x = 0; x < diagnosticReport.First().Length; x++)
            {
                if (o2.Count() > 1)
                {
                    var ones = Ones(o2, x);
                    o2 = ones.Count() >= o2.Count() / 2.0 ? ones : o2.Except(ones);
                }

                if (co2.Count() > 1)
                {
                    var ones = Ones(co2, x);
                    co2 = ones.Count() < co2.Count() / 2.0 ? ones : co2.Except(ones);
                }
            }
            return (o2.First().Long(2), co2.First().Long(2));
        }

        public static long LifeSupportRating(IEnumerable<string> diagnosticReport)
        {
            var (o2, co2) = PressRatings((IEnumerable<string>)diagnosticReport);
            return (o2 * co2);
        }


        static IEnumerable<string> Ones(IEnumerable<string> strs, int x)
        {
            return strs.Where(d => d[x] == '1');
        }
    }
}
