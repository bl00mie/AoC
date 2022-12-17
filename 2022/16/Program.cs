using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2022._16
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var valves = AoCUtil.GetAocInput(2022, 16)
                .Extract<(string valve, int rate, string valves)>(@"Valve (\w+) has flow rate=(\d+); tunnels? leads? to valves? (.+)")
                .ToDictionary(l => l.valve, l => new Valve(l.valve, l.rate, l.valves.Split(", ")));
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            //int max = 0;
            //var m = new List<(string, HashSet<string>, int)>(new[] { ("AA", new HashSet<string>(), 0) });
            //var notes = new Dictionary<string, int>();

            //for (int i=0; i<30; i++)
            //{
            //    var newM = new List<(string, HashSet<string>, int)>();
            //    foreach (var (name, state, pressure) in m)
            //    {
            //        if (!state.Contains(name) && valves[name].rate > 0)
            //        {
            //            var newState = state.ToHashSet();
            //            var newPressure = pressure + (30 - i - 1) * valves[name].rate;
            //            newState.Add(name);
            //            var key = name + string.Join("", newState.ToList().OrderBy(x => x));
            //            if (notes.ContainsKey(key) && pressure <= notes[key])
            //                continue;
            //            notes[key] = newPressure;
            //            newM.Add((name, newState, newPressure));
            //            max = Math.Max(max, newPressure);
            //        }

            //        foreach (var v in valves[name].valves)
            //        {
            //            var key = v + string.Join("", state.ToList().OrderBy(x => x));
            //            if (notes.ContainsKey(key) && pressure <= notes[key])
            //                continue;
            //            notes[key] = pressure;
            //            newM.Add((v, state, pressure));
            //        }
            //    }
            //    m = newM;
            //}
            //Ans(max);
            #endregion Part 1

            #region Part 2
            int max = 0;
            var m = new List<(string, string, HashSet<string>, int)>(new[] { ("AA", "AA", new HashSet<string>(), 0) });
            var notes = new Dictionary<string, int>();

            for (int i = 4; i < 30; i++)
            {
                var temp = new List<(string, string, HashSet<string>, int)>();
                foreach (var (name, elephant, state, pressure) in m)
                {
                    if (!state.Contains(name) && valves[name].rate > 0)
                    {
                        var newState = state.ToHashSet();
                        var newPressure = pressure + (30 - i - 1) * valves[name].rate;
                        newState.Add(name);
                        var key = string.Join("", new[]{name, elephant}.OrderBy(x => x)) + string.Join("", newState.ToList().OrderBy(x => x));
                        if (notes.ContainsKey(key) && pressure <= notes[key])
                            continue;
                        notes[key] = newPressure;
                        temp.Add((name, elephant, newState, newPressure));
                        max = Math.Max(max, newPressure);
                    }

                    foreach (var v in valves[name].valves)
                    {
                        var key = string.Join("", new[]{v, elephant}.OrderBy(x => x)) + string.Join("", state.ToList().OrderBy(x => x));
                        if (notes.ContainsKey(key) && pressure <= notes[key])
                            continue;
                        notes[key] = pressure;
                        temp.Add((v, elephant, state, pressure));
                    }
                }

               var newM = new List<(string, string, HashSet<string>, int)>();
               foreach (var (name, elephant, state, pressure) in temp)
               {
                   if (!state.Contains(elephant) && valves[elephant].rate > 0)
                   {
                       var newState = state.ToHashSet();
                       var newPressure = pressure + (30 - i - 1) * valves[elephant].rate;
                       newState.Add(elephant);
                       var key = string.Join("", new[]{name, elephant}.OrderBy(x => x)) + string.Join("", newState.ToList().OrderBy(x => x));
                       if (notes.ContainsKey(key) && pressure <= notes[key])
                           continue;
                       notes[key] = newPressure;
                       newM.Add((name, elephant, newState, newPressure));
                       max = Math.Max(max, newPressure);
                   }

                   foreach (var v in valves[elephant].valves)
                   {
                       var key = string.Join("", new[]{name, v}.OrderBy(x => x)) + string.Join("", state.ToList().OrderBy(x => x));
                       if (notes.ContainsKey(key) && pressure <= notes[key])
                           continue;
                       notes[key] = pressure;
                       newM.Add((name, v, state, pressure));
                   }
               }
                m = newM;
            }
            Ans2(max);
            #endregion
        }
    }

    record struct Valve (string name, int rate, string[] valves);
}

