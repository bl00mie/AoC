using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2022._11
{
    class Program : ProgramBase
    {
        static void Main()
        {
            var input = AoCUtil.GroupInput(2022, 11);

            var items = new List<List<long>>();
            var items2 = new List<List<long>>();
            var inspections = new List<int>();
            var inspections2 = new List<int>();
            var operations = new List<(int a, int b, bool plus)>();
            var tests = new List<(int div, int mt, int mf)>();
            long mod = 1;

            foreach (var group in input)
            {
                var monkey = group.ToList();
                
                items.Add(monkey[1][18..].Split(", ").Select(s => long.Parse(s)).ToList());
                items2.Add(items[^1].ToList());
                
                var opStrs = monkey[2][19..].Split(' ');
                operations.Add((
                    opStrs[0][0] == 'o' ? -1 : int.Parse(opStrs[0]),
                    opStrs[2][0] == 'o' ? -1 : int.Parse(opStrs[2]),
                    opStrs[1][0] == '+'));;

                var div = int.Parse(monkey[3][monkey[3].LastIndexOf(' ')..]);
                mod *= div;
                tests.Add((div, monkey[4][^1] - '0', monkey[5][^1] - '0'));

                inspections.Add(0);
                inspections2.Add(0);
            }


            foreach (int i in Enumerable.Range(0, 10000))
            {
                for (int id = 0; id < items.Count; id++)
                {
                    var (a, b, plus) = operations[id];
                    var (div, mt, mf) = tests[id];

                    if (i < 20)
                    {
                        foreach (var val in items[id])
                        {
                            var worry = Operation(val, a, b, plus) / 3;
                            items[worry % div == 0 ? mt : mf].Add(worry);
                            inspections[id]++;
                        }
                        items[id].Clear();
                    }

                    foreach (var val in items2[id])
                    {
                        var worry = Operation(val, a, b, plus) % mod;
                        items2[worry % div == 0 ? mt : mf].Add(worry);
                        inspections2[id]++;
                    }
                    items2[id].Clear();
                }
                
                if (i == 20)
                {
                    inspections.Sort();
                    Ans(inspections[^2] * inspections[^1]);
                }
            }

            inspections2.Sort();
            Ans2(1L * inspections2[^2] * inspections2[^1]);
        }

        static long Operation(long val, long a, long b, bool plus)
        {
            a = a < 0 ? val : a;
            b = b < 0 ? val : b;
            return plus ? a + b : a * b;
        }
    }
}

