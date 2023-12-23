using AoCUtil.Collections;

namespace AoC2023.Solutions
{
    internal class Day20 : BaseDay2023
    {
        readonly Dictionary<string, List<string>> Outputs = [];
        readonly Dictionary<string, char> Types = [];
        readonly DefaultDictionary<string, List<string>> Inputs = new(true);
        readonly DefaultDictionary<string, Dictionary<string, string>> Pulses = new(true);

        public override void ProcessInput()
        {
            Outputs.Clear();
            Types.Clear();
            Inputs.Clear();
            Pulses.Clear();
            var modules = Input.Extract<(string id, List<string> outputs)>(@"([%&]?[a-z]+) -> ((\w+),? ?)+");
            foreach (var (id, outputs) in modules)
            {
                Outputs[id] = outputs;
                Types[id[1..]] = id[0];
            }

            foreach (var (id, outputs) in Outputs)
            {
                Outputs[id] = outputs.Select(x =>
                {
                    if (Types.TryGetValue(x, out var type))
                        return $"{type}{x}";
                    return x;
                }).ToList();
                foreach (var output in Outputs[id])
                {
                    if (output[0] == '&')
                        Pulses[output][id] = "lo";
                    Inputs[output].Add(id);
                }
            }
        }

        public override dynamic Solve_1()
        {
            var lo = 0L;
            var hi = 0;
            var Q = new DLList<(string id, string from, string type)>();
            var on = new HashSet<string>();
            var previous = new Dictionary<string, int>();
            var count = new DefaultDictionary<string, int>();
            for (int i=0; i<1000; i++)
            {
                Q.Add(("broadcaster", "button", "lo"));
                while(Q.Count > 0)
                {
                    var (id, from, type) = Q.RemoveHead();
                    if (type == "lo") lo++;
                    else hi++;

                    if (!Outputs.ContainsKey(id)) continue;

                    if (id == "broadcaster")
                        foreach (var oid in Outputs[id])
                            Q.Add((oid, id, type));
                    else if (id[0] == '%')
                    {
                        var newType = "lo";
                        if (type == "hi") continue;
                        if (!on.Contains(id))
                        {
                            on.Add(id);
                            newType = "hi";
                        }
                        else
                            on.Remove(id);
                        foreach (var oid in Outputs[id])
                            Q.Add((oid, id, newType));
                    }
                    else if (id[0] == '&')
                    {
                        Pulses[id][from] = type;
                        var newType = Pulses[id].All(x => x.Value == "hi") ? "lo" : "hi";
                        foreach (var oid in Outputs[id])
                            Q.Add((oid, id, newType));
                    }
                }
            }
            return lo * hi;
        }

        public override dynamic Solve_2()
        {
            var Q = new DLList<(string id, string from, string type)>();
            var on = new HashSet<string>();
            var previous = new Dictionary<string, ulong>();
            var count = new DefaultDictionary<string, int>(true);
            var watch = Inputs[Inputs["rx"][0]];
            var cycles = new List<ulong>();

            for(ulong i=0; i<ulong.MaxValue; i++)
            {
                Q.Add(("broadcaster", "button", "lo"));
                while (Q.Count > 0)
                {
                    var (id, from, type) = Q.RemoveHead();
                    if (type == "lo")
                    {
                        if (previous.TryGetValue(id, out ulong pv) && count[id] == 2 && watch.Contains(id))
                            cycles.Add(i - pv);
                        if (cycles.Count == watch.Count)
                            return cycles.Aggregate((ulong)1, (a, b) => (a * b) / AoC.AoCUtil.GCD(a, b));
                        previous[id] = i;
                        count[id] += 1;
                    }

                    if (!Outputs.ContainsKey(id)) continue;

                    if (id == "broadcaster")
                        foreach (var oid in Outputs[id])
                            Q.Add((oid, id, type));
                    else if (id[0] == '%')
                    {
                        var newType = "lo";
                        if (type == "hi") continue;
                        if (!on.Contains(id))
                        {
                            on.Add(id);
                            newType = "hi";
                        }
                        else
                            on.Remove(id);
                        foreach (var oid in Outputs[id])
                            Q.Add((oid, id, newType));
                    }
                    else if (id[0] == '&')
                    {
                        Pulses[id][from] = type;
                        var newType = Pulses[id].All(x => x.Value == "hi") ? "lo" : "hi";
                        foreach (var oid in Outputs[id])
                            Q.Add((oid, id, newType));
                    }
                }
            }
            throw new Exception($"you counted to {ulong.MaxValue}?!");
        }
    }
}
