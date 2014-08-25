using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class Papadimitriou
    {
        public static bool IsSatisfiable(int n, IReadOnlyList<Clause> clauses)
        {
            var m = (int)Math.Log(n, 2);
            for (int i = 0; i < m; i++)
            {
                if (IsSatisfiableIteration(n, clauses)) return true;
            }

            return false;
        }

        private static bool IsSatisfiableIteration(int n, IReadOnlyList<Clause> clauses)
        {
            var assignment = new bool[n];
            var r = new Random();
            for (int i = 0; i < n; i++)
            {
                assignment[i] = r.Next(2) == 1;
            }

            var states = clauses.Select(c => IsSatisfied(c, assignment)).ToArray();
            var length = states.Length;

            var clausesByVariable =
                clauses.SelectMany((c, i) => new[] { new { Variable = c.Literal1.Index, Clause = i }, new { Variable = c.Literal2.Index, Clause = i } })
                    .ToLookup(x => x.Variable, x => x.Clause);

            var m = 2L * n * n;
            
            for (var t = 0; t < m; t++)
            {
                bool found = false;
                
                for (int i = 0; i < length; i++)
                    if (!states[i])
                    {
                        var clause = clauses[i];
                        var literal = (r.Next(2) == 1) ? clause.Literal1 : clause.Literal2;
                        assignment[literal.Index] = !assignment[literal.Index];
                        foreach (var index in clausesByVariable[literal.Index])
                        {
                            states[index] = IsSatisfied(clauses[index], assignment);
                        }

                        found = true;
                        break;
                    }

                if (!found)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsSatisfied(Clause c, IList<bool> assignment)
        {
            return IsSatisfied(c.Literal1, assignment) || IsSatisfied(c.Literal2, assignment);
        }

        private static bool IsSatisfied(Literal literal, IList<bool> assignment)
        {
            return literal.Negation != assignment[literal.Index];
        }
    }
}
