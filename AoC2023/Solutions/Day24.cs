using Microsoft.Z3;

namespace AoC2023.Solutions
{
    public class Day24() : BaseDay(2023)
    {
        const long MN = 200_000_000_000_000;
        const long MX = 400_000_000_000_000;
        List<Hailstone> Hailstones = [];
        public override void ProcessInput()
        {
            Hailstones = Input.Extract<Hailstone>(@"((\d+), (\d+), (\d+)) @ (([0-9-]+), ([0-9-]+), ([0-9-]+))").ToList();
        }


        public override dynamic Solve_1()
        {
            var ans = 0L;
            foreach (var (a, i) in Hailstones.Select((h,i) => (h,i)))
                foreach (var b in Hailstones[i..])
                {
                    if (a.a * b.b == a.b * b.a)
                        continue;
                    var denominator = (a.a * b.b - b.a * a.b);
                    var x = a.c / denominator * b.b - b.c / denominator * a.b;
                    var y = b.c / denominator * a.a - a.c / denominator * b.a;

                    if ((x >= MN) && (x <= MX) && (y >= MN) && (y <= MX))
                        if ((x - a.p.x) * a.v.dx >= 0 && (y - a.p.y) * a.v.dy >= 0 && (x - b.p.x) * b.v.dx >= 0 && (y - b.p.y) * b.v.dy >= 0)
                            ans++;
                }

            return ans;
        }

        public override dynamic Solve_2()
        {
            using Context ctx = new();
            var x = (ArithExpr)ctx.MkConst("x", ctx.MkIntSort());
            var y = (ArithExpr)ctx.MkConst("y", ctx.MkIntSort());
            var z = (ArithExpr)ctx.MkConst("z", ctx.MkIntSort());
            var vx = (ArithExpr)ctx.MkConst("vx", ctx.MkIntSort());
            var vy = (ArithExpr)ctx.MkConst("vy", ctx.MkIntSort());
            var vz = (ArithExpr)ctx.MkConst("vz", ctx.MkIntSort());
            var zero = (ArithExpr)ctx.MkNumeral(0, ctx.MkIntSort());
            List<ArithExpr> times = [];
            var solver = ctx.MkSolver();
            foreach (var i in Enumerable.Range(0, Hailstones.Count))
            {
                var ti = ctx.MkConst($"T{i}", ctx.MkIntSort());
                if (ti != null) times.Add((ArithExpr)ti);
                var hsx = (ArithExpr)ctx.MkNumeral(Hailstones[i].p.x, ctx.MkIntSort());
                var hsvx = (ArithExpr)ctx.MkNumeral(Hailstones[i].v.dx, ctx.MkIntSort());
                var hsy = (ArithExpr)ctx.MkNumeral(Hailstones[i].p.y, ctx.MkIntSort());
                var hsvy = (ArithExpr)ctx.MkNumeral(Hailstones[i].v.dy, ctx.MkIntSort());
                var hsz = (ArithExpr)ctx.MkNumeral(Hailstones[i].p.z, ctx.MkIntSort());
                var hsvz = (ArithExpr)ctx.MkNumeral(Hailstones[i].v.dz, ctx.MkIntSort());

                solver.Assert(ctx.MkEq(x + times[i] * vx - hsx - times[i] * hsvx, zero));
                solver.Assert(ctx.MkEq(y + times[i] * vy - hsy - times[i] * hsvy, zero));
                solver.Assert(ctx.MkEq(z + times[i] * vz - hsz - times[i] * hsvz, zero));
            }
            if (solver.Check() != Status.SATISFIABLE) throw new Exception("FUUUUUUUUUUUUUUUU!");
            return solver.Model.Eval(x + y + z).ToString();
        }

        record Hailstone((long x, long y, long z) p, (int dx, int dy, int dz) v)
        {
            public long a = v.dy;
            public long b = -v.dx;
            public long c = p.x * v.dy - p.y * v.dx;
        }
    }
}
