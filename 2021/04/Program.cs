using System.Collections.Generic;
using System.Linq;

namespace AoC._2021._04
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            var input = AoCUtil.GetAocInput(2021, 04).ToList();
            var balls = input.First().Split(',').Select(s => s.Int());
            input.RemoveAt(0);
            var cards = input.Chunk(6)
                .Select(chunk =>
                {
                    return chunk
                        .Where(line => !string.IsNullOrEmpty(line))
                        .Select(line => line.Split(' ')
                            .Where(item => !string.IsNullOrEmpty(item))
                            .Select(item => item.Int())
                            .ToArray())
                        .ToArray();
                })
                .ToList();
            #endregion

            var called = new List<int>(balls.Count());
            var sum = -1;
            var won = new bool[cards.Count];
            var winnerCount = 0;
            foreach (var ball in balls)
            {
                called.Add(ball);
                for (int i = 0; i < cards.Count; i++)
                {
                    if (won[i])
                        continue;
                    Mark(cards[i], ball);
                    if ((sum = Winner(cards[i], called)) >= 0)
                    {
                        won[i] = true;
                        if (winnerCount++ == 0)
                            Ans(sum * ball);
                        if (winnerCount == cards.Count)
                        {
                            Ans(sum * ball, 2);
                            return;
                        }
                        sum = -1;
                    }
                }
            }
        }

        static void Mark(int[][] card, int ball)
        {
            for (int i=0; i<5; i++)
                for (int j=0; j<5; j++)
                    if (card[i][j] == ball)
                        card[i][j] = -1;
        }

        static int Winner(int[][] card, List<int> balls)
        {
            for(int i=0; i<5; i++)
                if (!card[i].Any(d => d != -1) || !card.Select(d => d[i]).Any(d => d != -1))
                    return card.Select(row => row.Where(d => d != -1).Sum()).Sum();
            return -1;
        }
    }
}

