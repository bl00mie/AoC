using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2015._21
{
    class Program : ProgramBase
    {
        static void Main()
        {
            List<(string name, int cost, int dam, int def)> weapons = new List<(string, int, int, int)>
            {
                ("Dagger",     8,  4, 0),
                ("Shortsword", 10, 5, 0),
                ("Warhammer",  25, 6, 0),
                ("Longsword",  40, 7, 0),
                ("Greataxe",   74, 8, 0)
            };
            List < (string name, int cost, int dam, int def)> armors = new List<(string, int, int, int)>
            {
                ("No Armor",   0,   0, 0),
                ("Leather",    13,  0, 1),
                ("Chainmail",  31,  0, 2),
                ("Splintmail", 53,  0, 3),
                ("Bandedmail", 75,  0, 4),
                ("platemail",  102, 0, 5)
            };
            List<(string name, int cost, int dam, int def)> rings = new List<(string, int, int, int)>
            {
                ("No Left",    0,   0, 0),
                ("No Right",   0,   0, 0),
                ("Damage +1",  25,  1, 0),
                ("Damage +2",  50,  2, 0),
                ("Damage +3",  100, 3, 0),
                ("Defense +1", 20,  0, 1),
                ("Defense +2", 40,  0, 2),
                ("Defense +3", 80,  0, 3),
            };

            #region input

            var input = AoCUtil.GetAocInput(2015, 21).Select(s => s.Split(": ")).Select(sa => sa[1]).Select(int.Parse).ToArray();
            double bossHP = input[0];
            int bossDam = input[1];
            int bossDef = input[2];

            var gear = from w in weapons
                       from a in armors
                       from r1 in rings
                       from r2 in rings
                       where r1 != r2
                       select new
                       {
                           Names = string.Format("{0}, {1}, {2}, {3}",w.name, a.name, r1.name, r2.name),
                           Dam = w.dam + r1.dam + r2.dam,
                           Def = a.def + r1.def + r2.def,
                           Cost = w.cost + a.cost + r1.cost + r2.cost
                       };
            #endregion

            #region Part 1

            Func<int, int, bool> isWinr = (dam, def) =>
            {
                var turns = (int)Math.Ceiling(bossHP / Math.Max(dam - bossDef, 1));
                return 100 - Math.Max(bossDam - def, 1) * (turns - 1) > 0;
                //var hp = 100;
                //var bhp = bossHP;
                //while (true)
                //{
                //    bhp -= Math.Max(1, dam - bossDef);
                //    if (bossHP <= 0) return true;
                //    hp -= Math.Max(1, bossDam - def);
                //    if (hp <= 0) return false;
                //}

            };

            Ans(gear.Where(g => isWinr(g.Dam, g.Def)).Select(g => g.Cost).Min());

            #endregion Part 1

            #region Part 2

            Ans(gear.Where(g => !isWinr(g.Dam, g.Def)).OrderBy(g=>g.Cost).Select(g => {
                //Log(string.Format("Dam:{0} Def:{1} Cost: {2} Items:{3}", g.Dam, g.Def, g.Cost, g.Names));
                return g.Cost;
            } ).Max(), 2);
            #endregion
        }
    }
}

