using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC._2021._16
{
    class Program : ProgramBase
    {
        static long total;
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var bits = string.Join(string.Empty, AoCUtil.GetAocInput(2021, 16).First()
                .Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var p = 0;
            var packet = Parse(bits, ref p);
            Ans(total);
            #endregion Part 1

            #region Part 2

            Ans2(Evaluate(packet));
            #endregion
        }

        static long Evaluate(Packet p)
        {
            return p.typeId switch
            {
                4 => p.value,
                0 => p.subPackets.Sum(p => Evaluate(p)),
                1 => p.subPackets.Aggregate(1L, (x,p) => x * Evaluate(p)),
                2 => p.subPackets.Min(p => Evaluate(p)),
                3 => p.subPackets.Max(p => Evaluate(p)),
                5 => Evaluate(p.subPackets[0]) >  Evaluate(p.subPackets[1]) ? 1 : 0,
                6 => Evaluate(p.subPackets[0]) <  Evaluate(p.subPackets[1]) ? 1 : 0,
                7 => Evaluate(p.subPackets[0]) == Evaluate(p.subPackets[1]) ? 1 : 0,
                _ => throw new Exception("wtaf")
            };
        }

        static long Product(List<Packet> packets)
        {
            long prod = 1;
            foreach (var p in packets)
                prod *= Evaluate(p);
            return prod;
        }

        static Packet Parse(string bits, ref int p)
        {
            total += Read(bits, ref p, 3).Int(2);
            var packet = new Packet
            {
                typeId = Read(bits, ref p, 3).Int(2)
            };
            if (packet.typeId == 4)
            {
                var sb = new StringBuilder();
                var s = string.Empty;
                do
                {
                    s = Read(bits, ref p, 5);
                    sb.Append(s[1..]);
                }
                while (s[0] == '1');
                packet.value = sb.ToString().Long(2);
            }
            else
                if (Read(bits, ref p, 1) == "1")
                {
                    var len = Read(bits, ref p, 11).Int(2);
                    for (int i = 0; i < len; i++)
                        packet.subPackets.Add(Parse(bits, ref p));
                }
                else
                {
                    var len = Read(bits, ref p, 15).Int(2);
                    var sp = 0;
                    var subBits = Read(bits, ref p, len);
                    while (sp < len)
                        packet.subPackets.Add(Parse(subBits, ref sp));
                }
            return packet;
        }

        public static string Read(string bits, ref int p, int len)
        {
            var data = bits[p..(p + len)];
            p += len;
            return data;
        }

        record Packet
        {
            public int typeId;
            public long value;
            public List<Packet> subPackets = new();
        }
    }
}

