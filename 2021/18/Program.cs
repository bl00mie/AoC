using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021._18
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var snailfishNumbers = AoCUtil.GetAocInput(2021, 18)
                .Select(s => SnailfishNumber.Parse(s))
                .ToList();

            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion

            #region Part 1
            var sum = snailfishNumbers[0];
            for (int i = 1; i < snailfishNumbers.Count; i++)
                sum += snailfishNumbers[i];
           Ans(sum.Magnitude());
            #endregion Part 1

            #region Part 2
            
            var maxMag = int.MinValue;
            for (int i=0; i<snailfishNumbers.Count; i++)
                for (int j=0; j<snailfishNumbers.Count; j++)
                    if (i != j)
                        maxMag = Math.Max(maxMag, (snailfishNumbers[i] + snailfishNumbers[j]).Magnitude());
            Ans2(maxMag);
            #endregion
        }
    }

    abstract class SnailfishNumber : IEnumerable<SnailfishNumber>
    {
        public SnailfishPair parent;
        public int depth
        {
            get
            {
                if (parent == null) return 0;
                return parent.depth + 1;
            }
        }

        public static SnailfishNumber Parse(string s)
        {
            if (s[0] != '[')
                return new SnailfishValue(int.Parse(s));
            var sub = s[1..^1];
            int p;
            if (sub[0] != '[')
                p = sub.IndexOf(',');
            else
            {
                var open = 1;
                p = 1;
                while(open > 0)
                {
                    open += sub[p++] switch
                    {
                        '[' => 1,
                        ']' => -1,
                        _ => 0
                    };
                }
            }
            return new SnailfishPair(Parse(sub[..p]), Parse(sub[(p+1)..]));
        }

        public void Replace(SnailfishNumber replacement)
        {
            replacement.parent = parent;
            if (parent == null)
                return;
            if (parent.left == this)
                parent.left = replacement;
            else
                parent.right = replacement;

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract IEnumerator<SnailfishNumber> GetEnumerator();

        public abstract bool IsValue();
        public abstract int Magnitude();
        public abstract void Reduce();
        public abstract bool NeedsReduction();

        public static SnailfishNumber operator +(SnailfishNumber left, SnailfishNumber right)
        {
            var leftCopy = Parse(left.ToString());
            var rightCopy = Parse(right.ToString());
            var sum = new SnailfishPair(leftCopy, rightCopy);
            List<SnailfishNumber> reductions = sum.Where(sfn => sfn.NeedsReduction()).OrderBy(sfn => sfn is SnailfishValue).ToList();
            while (reductions.Any())
            {
                reductions.First().Reduce();
                reductions = sum.Where(sfn => sfn.NeedsReduction()).OrderBy(sfn => sfn is SnailfishValue).ToList();
            }
            return sum;
        }
    }

    class SnailfishPair : SnailfishNumber
    {
        public SnailfishNumber left;
        public SnailfishNumber right;

        public SnailfishPair(SnailfishNumber left, SnailfishNumber right)
        {
            this.left = left;
            this.right = right;
            left.parent = this;
            right.parent = this;
        }

        public override bool IsValue() => false;
        public override string ToString() => $"[{left},{right}]";
        public override bool NeedsReduction() => depth > 3;

        public override void Reduce()
        {
            if (!NeedsReduction())
                return;

            var n = this;
            while(n.parent?.right == n)
                n = n.parent;
            var rightNumber = n?.parent?.right;
            if (rightNumber != null)
            {
                while (rightNumber is SnailfishPair)
                    rightNumber = (rightNumber as SnailfishPair).left;
                (rightNumber as SnailfishValue).value += (right as SnailfishValue).value;
            }

            n = this;
            while (n.parent?.left == n)
                n = n.parent;
            var leftNumber = n?.parent?.left;
            if (leftNumber != null)
            {
                while (leftNumber is SnailfishPair)
                    leftNumber = (leftNumber as SnailfishPair).right;
                (leftNumber as SnailfishValue).value += (left as SnailfishValue).value;
            }

            Replace(new SnailfishValue(0));
        }

        public override int Magnitude() => 3 * left.Magnitude() + 2 * right.Magnitude();

        public override IEnumerator<SnailfishNumber> GetEnumerator()
        {
            foreach (var snf in left)
                yield return snf;
            yield return this;
            foreach (var snf in right)
                yield return snf;
        }
    }

    class SnailfishValue : SnailfishNumber
    {
        public int value;
        public SnailfishValue(int value)
        {
            this.value = value;
        }

        public override bool IsValue() => true;
        public override string ToString() => $"{value}";
        public override int Magnitude() => value;
        public override bool NeedsReduction() => value > 9;
        
        public override void Reduce()
        {
            if (!NeedsReduction())
                return;
            var replacement = new SnailfishPair(new SnailfishValue(value / 2), new SnailfishValue(value / 2 + value % 2));
            Replace(replacement);
        }

        public override IEnumerator<SnailfishNumber> GetEnumerator()
        {
            yield return this;
        }
    }

}