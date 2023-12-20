using System.Security.Cryptography;
using System.Text;

namespace AoC2016.Solutions.Solutions
{
    internal class Day05 : BaseDay2016
    {
        string doorId = string.Empty;
        public override void ProcessInput()
        {
            doorId = Input.First();
        }

        public override dynamic Solve_1()
        {
            var password = string.Empty;
            var salt = 1L;
            while (password.Length < 8)
            {
                var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{doorId}{salt++}"));
                if (hash[0] == 0 && hash[1] == 0 && hash[2] < 0x10)
                    password = $"{password}{hash[2]:x}";
            }
            return password;
        }

        public override dynamic Solve_2()
        {
            var password = new Dictionary<byte, byte>();
            var salt = 1L;
            while(password.Count < 8)
            {
                var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{doorId}{salt++}"));
                if (hash[0] == 0 && hash[1] == 0 && hash[2] < 0x10)
                {
                    var i = hash[2];
                    if (i > 7 || password.ContainsKey(i)) continue;
                    var val = (byte)(hash[3] / 16);
                    password[i] = val;
                }
            }
            return string.Join("", password.OrderBy(pair => pair.Key).Select(x => string.Format("{0:x}", x.Value)));
        }
    }
}
