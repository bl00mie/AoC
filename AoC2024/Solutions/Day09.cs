using Nito.Collections;

namespace AoC2024.Solutions
{
    public class Day09() : BaseDay(2024)
    {
        string DiskMap;
        public override void ProcessInput()
        {
            DiskMap = Input.First();
        }

        long Solve(bool part2 = false)
        {
            var ID = 0;
            var blocks = new Deque<(int pos, int sz, int id)>();
            var space = new List<(int pos, int sz)>();
            var disk = new List<int>();
            var POS = 0;
            foreach (var (i, sz) in DiskMap.Select((c, i) => (i, (int)(c - '0'))))
            {
                if (i % 2 == 0)
                {
                    if (part2)
                        blocks.AddToBack((POS, sz, ID));
                    foreach (var j in Enumerable.Range(0, sz))
                    {
                        disk.Add(ID);
                        if (!part2)
                            blocks.AddToBack((POS, 1, ID));
                        POS++;
                    }
                    ID++;
                }
                else
                {
                    space.Add((POS, sz));
                    foreach (var j in Enumerable.Range(0, sz))
                    {
                        disk.Add(-1);
                        POS++;
                    }
                }
            }

            while(blocks.Count > 0)
            {
                var (pos, sz, id) = blocks.RemoveFromBack();
                foreach (var (i, spacePos, spaceSz) in space.Select((v, i) => (i, v.pos, v.sz)))
                {
                    if (spacePos < pos && sz <= spaceSz)
                    foreach (var j in Enumerable.Range(0, sz))
                    {
                        disk[pos + j] = -1;
                        disk[spacePos + j] = id;
                    }
                    space[i] = (spacePos + sz, spaceSz - sz);
                    break;
                }
            }

            var ret = 0L;
            foreach (var (v, i) in disk.Select((v, i) => (v, i)).Where(pair => pair.v >= 0))
                ret += v * i;

            return ret ;
        }

        public override dynamic Solve_1()
        {
            return Solve();
        }

        public override dynamic Solve_2()
        {
            return Solve(true);
        }
    }
}
