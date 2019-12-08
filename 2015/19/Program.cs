using System.Linq;

namespace AoC._2015._19
{
    class Program : ProgramBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Common Practices and Code Improvements", "RECS0063:Warns when a culture-aware 'StartsWith' call is used by default.", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Common Practices and Code Improvements", "RECS0060:Warns when a culture-aware 'IndexOf' call is used by default.", Justification = "<Pending>")]
        static void Main()
        {
            #region input

            var lines = AoCUtil.GetAocInput(2015, 19).ToList();

            var replacements = lines
                                .Select(s => s.Split(' '))
                                .Where(a => a.Length == 3)
                                .Select(a => new[] { a[0], a[2] })
                                .ToList();
            var molecule = lines[^1];

            #endregion

            #region Part 1

            Ans((from pos in Enumerable.Range(0, molecule.Length)
                   from rep in replacements
                   let a = rep[0]
                   let b = rep[1]
                   where molecule[pos..].StartsWith(a)
                   select molecule[..pos] + b + molecule[(pos + a.Length)..]).Distinct().Count());

            #endregion Part 1

            #region Part 2

            var target = molecule;
            var mutations = 0;

            while (target != "e")
            {
                var tmp = target;
                foreach (var rep in replacements)
                {
                    var index = target.IndexOf(rep[1]);
                    if (index >= 0)
                    {
                        target = target.Substring(0, index) + rep[0] + target.Substring(index + rep[1].Length);
                        mutations++;
                    }
                }

                if (tmp == target)
                {
                    target = molecule;
                    mutations = 0;
                    replacements = AoCUtil.Shuffle(replacements).ToList();
                }
            }
            Ans(mutations, 2);

            #endregion
        }


    }
}

