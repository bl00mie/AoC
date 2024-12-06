namespace AoC2023.Solutions
{
    internal class Day09() : BaseDay(2023)
    {
        static int Extrapolate(int[] ints)
        {
            if (ints.All(i => i == 0)) return 0;
            return ints[^1] + Extrapolate(ints.SlidingWindow().Select(p => p.b - p.a).ToArray());
        }

        public override dynamic Solve_1()
        {
            return Input.Select(s => s.GetInts(' ').ToArray()).Sum(Extrapolate);
        }

        public override dynamic Solve_2()
        {
            return Input.Select(s => s.GetInts(' ')).Select(ints => ints.Reverse().ToArray()).Sum(Extrapolate);
        }
    }
}
