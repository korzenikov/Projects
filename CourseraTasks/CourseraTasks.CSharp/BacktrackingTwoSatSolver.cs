using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class BacktrackingTwoSatSolver
    {
        public static bool IsSatisfiable(IReadOnlyList<Clause> clauses)
        {
            var problems = new Queue<Problem>();
            problems.Enqueue(new Problem(Enumerable.Repeat(ClauseStates.Undetermined, clauses.Count).ToArray()));
            while (problems.Count != 0)
            {
                var problem = problems.Dequeue();
                var index = Choose(clauses, problem.States);
                foreach (var subproblem in Expand(clauses, problem, index))
                {
                    if (subproblem.States.All(state => state == ClauseStates.True))
                    {
                        return true;
                    }

                    if (subproblem.States.All(state => state != ClauseStates.False))
                    {
                        problems.Enqueue(subproblem);
                    }
                }
            }

            return false;
        }

        private static int Choose(IEnumerable<Clause> clauses, IEnumerable<ClauseStates> problem)
        {
            var clausesWithStates = clauses.Zip(problem, (clause, state) => new { Clause = clause, State = state }).ToArray();
            foreach (var pair in clausesWithStates)
            {
                if (pair.State == ClauseStates.LeftFalse)
                {
                    return pair.Clause.Literal2.Index;
                }

                if (pair.State == ClauseStates.RightFalse)
                {
                    return pair.Clause.Literal1.Index;
                }
            }

            return
                clausesWithStates
                    .First(x => x.State == ClauseStates.Undetermined)
                    .Clause.Literal1.Index;
        }

        private static IEnumerable<Problem> Expand(IEnumerable<Clause> clauses, Problem problem, int variable)
        {
            yield return new Problem(
                Expand(clauses, problem.States, variable, false));

            yield return new Problem(
                Expand(clauses, problem.States, variable, true));
        }

        private static IReadOnlyCollection<ClauseStates> Expand(IEnumerable<Clause> clauses, IEnumerable<ClauseStates> problem, int variable, bool value)
        {
            return clauses.Zip(problem, (clause, state) => new { Clause = clause, State = state }).Select(
                pair =>
                    {
                        if (pair.State == ClauseStates.True)
                        {
                            return ClauseStates.True;
                        }

                        if (CanEliminateClause(pair.Clause, variable, value))
                        {
                            return ClauseStates.True;
                        }

                        var state = pair.State;
                        if (!state.HasFlag(ClauseStates.LeftFalse) && IsFalse(pair.Clause.Literal1, variable, value))
                        {
                            state |= ClauseStates.LeftFalse;
                        }

                        if (!state.HasFlag(ClauseStates.RightFalse) && IsFalse(pair.Clause.Literal2, variable, value))
                        {
                            state |= ClauseStates.RightFalse;
                        }

                        return state;
                    }).ToArray();
        }

        private static bool CanEliminateClause(Clause clause, int variable, bool value)
        {
            if (clause.Literal1 != null && clause.Literal1.Index == variable && !clause.Literal1.Negation == value) return true;
            if (clause.Literal2 != null && clause.Literal2.Index == variable && !clause.Literal2.Negation == value) return true;

            return false;
        }

        private static bool IsFalse(Literal literal, int variable, bool value)
        {
            return literal != null && literal.Index == variable && literal.Negation == value;
        }
    }
}
