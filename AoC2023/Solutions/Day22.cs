using AoCUtil.Collections;

namespace AoC2023.Solutions
{
    internal class Day22() : BaseDay(2023)
    {
        (int[] a, int[] b)[] Bricks = [];
        const int X = 0;
        const int Y = 1;
        const int Z = 2;
        public override void ProcessInput()
        {
            Bricks = [.. Input.Extract<(int[] a, int[] b)>(@"((\d+),?)+~((\d+),?)+")
                .Select(p => p.a[Z] < p.b[Z] ? ([.. p.a], [.. p.b]) : (p.b.ToArray(), p.a.ToArray()))
                .OrderBy(p => Math.Min(p.Item1[Z], p.Item2[Z]))];

            HashSet<(int, int, int)> allblocks = [];
            foreach (var brick in Bricks)
                foreach (var block in GetBlocks(brick))
                    if (!allblocks.Add((block[X], block[Y], block[Z]))) throw new Exception("fuuuuuu");

        }

        private static IEnumerable<int[]> GetBlocks((int[] a, int[] b) brick)
        {
            int[] template = [brick.a[X], brick.a[Y], brick.a[Z]];
            var variable = brick.a[X] != brick.b[X] ? X : brick.a[Y] != brick.b[Y] ? Y : Z;
            template[variable] = brick.a[variable];

            var delta = brick.b[variable] - brick.a[variable];
            var dir = delta < 0 ? -1 : 1;
            delta = Math.Abs(delta) + 1;
            
            foreach (var i in Enumerable.Range(0, delta))
                yield return new List<int>(template) { [variable] = brick.a[variable] + i * dir }.ToArray();
        }

        public override dynamic Solve_1()
        {
//            List<((int x, int y, int z) a, (int x, int y, int z) b)> fallenBricks = [];
//            var columns = new DefaultDictionary<(int x, int y), List<int>>(true);
//            foreach (var brick in Bricks)
//            {
//                var blocks = GetBlocks(brick).ToArray();
//                if (blocks[0].x == blocks[1].x && blocks[0].y == blocks[1].y)
//                {
//                    var col = columns[(blocks[0].x, blocks[1].y)];
////                    fallenBricks.Add(((brick.a.x, brick.a.y, col[^1] + 1), (brick.a.x, brick.a.y, col[^1] + )))

//                    foreach (var _ in Enumerable.Range(0, blocks.Length))
//                        col.Add(col[^1] + 1);
//                }
//                else
//                {
//                    var basez = blocks.Select(b => columns[(b.x, b.y)].Last()).Max() + 1;
//                    foreach (var block in blocks)
//                        columns[(block.x, block.y)].Add(basez);

//                }

//                foreach (var block in blocks)
//                {
                    
//                }
//            }

            return "";
        }

        public override dynamic Solve_2()
        {
            return "";
        }
    }
}
