namespace AoC2019.Solutions
{
    internal class Day01() : BaseDay(2019)
    {
        int[] Masses = [];
        public override void ProcessInput()
        {
            Masses = Input.Select(int.Parse).ToArray();
        }

        public override dynamic Solve_1()
        {
            return Masses.Sum(m => m / 3 - 2);
        }

        public override dynamic Solve_2()
        {
            return Masses.Sum(m =>
            {
                int fuel = m/3-2;
                int moar = fuel;
                while ((moar = moar / 3 - 2) > 0)
                    fuel += moar;
                return fuel;
            });
        }
    }
}
