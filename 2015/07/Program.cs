using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2015._07
{
    class Program
    {
        static void Main(string[] args)
        {
            var instrs = AoCUtil.GetAocInput(2015, 7).Select(L => L.Split(' '))
                .ToDictionary(sa => sa.Last(),
                                    sa => {
                                        if (sa.Length == 3)
                                        {
                                            return new Tuple<string, string[]>("SET", new string[] { sa[0] }); 
                                        }
                                        if (sa.Length == 4)
                                        {
                                            return new Tuple<string,string[]>("NOT", new string[] { sa[1] });
                                        }
                                        else
                                        {
                                            return new Tuple<string, string[]>(sa[1], new string[] { sa[0], sa[2] });
                                        }
                                    });

            #region Part 1

            var gm = new GateMachine();
            foreach (var key in instrs.Keys)
            {
                gm.Eval(instrs, key);
            }

            #endregion Part 1

            #region Part 2

            GateMachine.wires.Clear();
            instrs["b"] = new Tuple<string, string[]>("SET", new string[] { "3176" });
            foreach(var key in instrs.Keys)
            {
                gm.Eval(instrs, key);
            }

            #endregion
        }
    }

    public class GateMachine
    {
        public static Dictionary<string, ushort> wires = new Dictionary<string, ushort>();
        public Dictionary<string, Action<string[], string>> gates = new Dictionary<string, Action<string[], string>>()
        {
            { "AND", (input, wire) =>
                {
                    ushort a,b;
                    a = ushort.TryParse(input[0], out ushort x) ? x : wires[input[0]];
                    b = ushort.TryParse(input[1], out x) ? x : wires[input[1]];
                    wires[wire] = (ushort)(a & b);
                }
            },
            { "OR", (input, wire) => { wires[wire] = (ushort)(wires[input[0]] | wires[input[1]]); } },
            { "LSHIFT", (input, wire) => { wires[wire] = (ushort)(wires[input[0]] << ushort.Parse(input[1])); } },
            { "RSHIFT", (input, wire) => { wires[wire] = (ushort)(wires[input[0]] >> ushort.Parse(input[1])); } },
            { "NOT", (input, wire) => { wires[wire] = (ushort)~wires[input[0]]; } },
            { "SET", (input, wire) =>
                {
                    ushort a;
                    a = ushort.TryParse(input[0], out ushort x) ? x : wires[input[0]];
                    wires[wire] = a;
                }
            }
        };

        public ushort Eval(Dictionary<string, Tuple<string, string[]>> instrs, string key)
        {
            if (wires.ContainsKey(key)) return wires[key];
            var instr = instrs[key];
            foreach (string wire in instr.Item2)
            {
                if (!int.TryParse(wire, out int _))
                    try
                    {
                        Eval(instrs, wire);
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("amg");
                    }
            }
            try
            {
                gates[instr.Item1](instr.Item2, key);
            }
            catch(Exception)
            {
                Console.WriteLine("zomg");
            }
            //PrintState();
            if (key == "a") Console.WriteLine("FINISHED " + wires["a"]);
            return wires[key];
        }

        public void PrintState()
        {
            Console.WriteLine("----------");
            foreach (var key in wires.Keys.OrderBy(x=>x))
            {
                Console.WriteLine(string.Format("{0}: {1}", key, wires[key]));
            }
            Console.WriteLine("----------");
        }
    }
}
