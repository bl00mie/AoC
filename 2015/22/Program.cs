using System;
using System.Linq;
using System.Collections.Generic;

namespace AoC._2015._22
{
    public class Program : ProgramBase
    {
        static readonly Random Rand = new Random();

        static int pHP, pMana;
        static int bHP, bDam;

        static int minWin = int.MaxValue;
        static int spent;
        static bool hardMode;
        const int ROUNDS = 50000;

        static List<Spell> effects = new List<Spell>();
        static readonly List<Spell> Spells = new List<Spell>
        {
            new Spell { Name = "MM", Cost =  53, Duration = 1, Damage = 4, Armor = 0, Heal = 0, Mana = 0 },
            new Spell { Name = "D",  Cost =  73, Duration = 1, Damage = 2, Armor = 0, Heal = 2, Mana = 0 },
            new Spell { Name = "S",  Cost = 113, Duration = 6, Damage = 0, Armor = 7, Heal = 0, Mana = 0 },
            new Spell { Name = "P",  Cost = 173, Duration = 6, Damage = 3, Armor = 0, Heal = 0, Mana = 0 },
            new Spell { Name = "R",  Cost = 229, Duration = 5, Damage = 0, Armor = 0, Heal = 0, Mana = 101 }
        };

        public static void Main()
        {
            #region Part 1

            for (int x = 0; x < ROUNDS; ++x)
            {
                Reset();
                var victor = Fight();
                if (victor == Victor.Player)
                    if (spent < minWin) minWin = spent;
            }
            Ans(minWin);

            #endregion

            #region Part 2

            hardMode = true;
            minWin = int.MaxValue;
            for (int i = 0; i < ROUNDS; ++i)
            {
                Reset();
                var victor = Fight();
                if (victor == Victor.Player)
                    if (spent < minWin) minWin = spent;
            }
            Ans(minWin, 2);

            #endregion
        }

        public static void Reset()
        {
            pHP = 50; pMana = 500;
            bHP = 55; bDam = 8;
            spent = 0;
            effects.Clear();
        }

        public static Victor Fight()
        {
            Victor victor;
            while (true)
            {
                if (hardMode)
                {
                    pHP--;
                    if ((victor = Winner()) != Victor.None) return victor;
                }

                ApplyEffects();
                if ((victor = Winner()) != Victor.None) return victor;

                var spell = RandomSpell();
                if (spell == null) return Victor.None;

                PlayerTurn(spell);
                if ((victor = Winner()) != Victor.None) return victor;

                ApplyEffects();
                if ((victor = Winner()) != Victor.None) return victor;

                BossTurn();
                if ((victor = Winner()) != Victor.None) return victor;
            }
        }

        public static void ApplyEffects()
        {
            bHP -= effects.Sum(x => x.Damage);
            pMana += effects.Sum(x => x.Mana);

            foreach (var effect in effects) effect.Timer++;

            effects = effects.Where(x => x.Timer < x.Duration).ToList();
        }

        public static Spell RandomSpell()
        {
            var possibleSpells = Spells
                .Where(x => effects.All(y => y.Name != x.Name))
                .Where(x => x.Cost <= pMana)
                .Where(x => minWin > x.Mana + spent)
                .ToList();

            if (possibleSpells.Any())
            {
                var spell = possibleSpells[Rand.Next(possibleSpells.Count)];
                pMana -= spell.Cost;
                spent += spell.Cost;
                spell.Timer = 0;
                return spell;
            }
            return null;
        }

        public static void PlayerTurn(Spell spell)
        {
            if (spell.Duration > 1)
            {
                spell.Timer = 0;
                effects.Add(spell);
            }
            else
            {
                bHP -= spell.Damage;
                pHP += spell.Heal;
            }
        }

        public static void BossTurn()
        {
            pHP -= Math.Max(1, bDam - effects.Sum(x=>x.Armor));
        }

        public static Victor Winner()
        {
            if (pHP <= 0)
                return Victor.Boss;
            if (bHP <= 0)
                return Victor.Player;
            return Victor.None;
        }
    }

    public class Spell
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Cost { get; set; }
        public int Damage { get; set; }
        public int Heal { get; set; }
        public int Armor { get; set; }
        public int Mana { get; set; }
        public int Timer { get; set; }
    }

    public enum Victor
    {
        Player,
        Boss,
        None
    }
}
