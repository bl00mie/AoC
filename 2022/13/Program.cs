using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AoC._2022._13
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GroupInput(2022, 13).ToList();

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var ans = 0;
            var comp = new PacketComparer();
            foreach (var i in Enumerable.Range(0, input.Count()))
            {
                var l = JsonDocument.Parse(input[i].First()).RootElement;
                var r = JsonDocument.Parse(input[i].Last()).RootElement;
                ans += comp.Compare(l, r) == -1 ? i + 1 : 0;
            }
            Ans(ans);
            #endregion Part 1

            #region Part 2
            var two = JsonDocument.Parse("[[2]]").RootElement;
            var six = JsonDocument.Parse("[[6]]").RootElement;

            var all = input.SelectMany(g => g.ToList().Select(i => JsonDocument.Parse(i).RootElement))
                .Concat(new[] { two, six })
                .ToList();
            all.Sort(comp);

            Ans2((all.FindIndex(x => x.Equals(two)) + 1) * (all.FindIndex(x => x.Equals(six)) + 1));
            #endregion
        }


        class PacketComparer : IComparer<JsonElement>
        {
            public int Compare(JsonElement l, JsonElement r)
            {
                if (l.ValueKind == JsonValueKind.Number && r.ValueKind == JsonValueKind.Number)
                    return l.GetInt32().CompareTo(r.GetInt32());

                if (l.ValueKind == JsonValueKind.Array)
                {
                    if (r.ValueKind == JsonValueKind.Number)
                        return Compare(l, JsonDocument.Parse($"[{r}]").RootElement);

                    var ll = l.GetArrayLength();
                    var rl = r.GetArrayLength();

                    for (int i = 0; i < ll && i < rl; i++)
                    {
                        if (rl <= i) break;
                        var ct = Compare(l[i], r[i]);
                        if (ct < 0)
                            return -1;
                        if (ct > 0)
                            return 1;
                    }
                    if (ll > rl)
                        return 1;
                    if (ll < rl)
                        return -1;
                    return 0;
                }
                else
                    return Compare(JsonDocument.Parse($"[{l}]").RootElement, r);
            }
        }
    }
}

