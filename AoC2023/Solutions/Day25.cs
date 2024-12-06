using AoCUtil.Collections;
using QuikGraph;
using QuikGraph.Algorithms;
using System.Data;

namespace AoC2023.Solutions
{
    internal class Day25() : BaseDay(2023)
    {
        DefaultDictionary<string, HashSet<string>> Components = new(true);
        public override void ProcessInput()
        {
            foreach (var (component, wired) in Input.Extract<(string component, List<string> wired)>(@"(\w+): ((\w+) ?)+"))
                foreach (var w in wired)
                {
                    Components[component].Add(w);
                    Components[w].Add(component);
                }
        }

        public override dynamic Solve_1()
        {
            AdjacencyGraph<string, Edge<string>> graph = new();
            foreach (var vertex in Components.Keys)
                graph.AddVertex(vertex);
            foreach (var pair in Components)
                foreach (var b in pair.Value)
                    graph.AddEdge(new(pair.Key, b));

            DefaultDictionary<string, int> frequency = new(true);

            var keys = Components.Keys.ToArray();
            Random r = new();
            foreach (var _ in Enumerable.Range(0, 1000))
            {
                var ia = r.Next(keys.Length);
                var ib = r.Next(keys.Length);
                if (ia == ib) continue;
                if (graph.ShortestPathsDijkstra((e) => 1, keys[ia])(keys[ib], out var result))
                {
                    foreach (var e in result)
                        frequency[e.Source]++;
                }
            }

            var top = frequency.OrderByDescending(p => p.Value).Select(p => p.Key).Take(6).ToList();

            while(top.Count > 0)
            {
                var a = top[0];
                foreach (var b in top.Skip(1))
                    if (Components[a].Contains(b))
                    {
                        graph.TryGetEdge(a, b, out var e);
                        graph.RemoveEdge(e);
                        graph.TryGetEdge(b, a, out var e2);
                        graph.RemoveEdge(e2);
                        top.Remove(a); top.Remove(b);
                        break;
                    }
            }

            var dijk = graph.ShortestPathsDijkstra((e) => 1, Components.Keys.First());

            var n = Components.Keys.First();
            var count = Components.Keys.Count(b => dijk(b, out var _)) + 1;
            return count * (Components.Count - count);
        }

        public override dynamic Solve_2()
        {
            return "Merry Christmas!";
        }
    }
}
