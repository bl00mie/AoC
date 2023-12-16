namespace AoC2023.Solutions
{
    internal class Day15 : BaseDay2023
    {
        string line = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
        public override void ProcessInput()
        {
            line = Input.First();
        }

        int Hash(string s) => s.Aggregate(0, (a, b) => (a + b) * 17 % 256);

        public override string Solve_1() => line.Split(',').Sum(Hash).ToString();

        public override dynamic Solve_2()
        {
            var boxes = new List<List<(string label, int fl)>>();
            for (int i = 0; i < 256; i++)
                boxes.Add([]);
            foreach (var cmd in line.Split(','))
            {
                if (cmd[^1] == '-')
                {
                    var label = cmd[..^1];
                    var hash = Hash(cmd[..^1]);
                    boxes[hash] = boxes[hash].Where(x => x.label != label).ToList();
                }
                else
                {
                    int fl = cmd[^1] - '0';
                    var label = cmd[..^2];
                    var hash = Hash(label);
                    if (boxes[hash].Any(x => x.label == label))
                        boxes[hash] = boxes[hash].Select(x => (x.label, x.label == label ? fl : x.fl)).ToList();
                    else
                        boxes[hash].Add((label, fl));
                }
            }

            return boxes.Select((box, i) =>
                    box.Select((lens, j) => lens.fl * (i + 1) * (j + 1)).Sum()).Sum();
        }
    }
}
