using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class TwoSatSolver
    {
        public static bool IsSatisfiable(IReadOnlyCollection<Clause> clauses)
        {
            var graph = new DirectedGraph<int>();

            foreach (var clause in clauses)
            {
                var literal1 = clause.Literal1;
                var literal2 = clause.Literal2;
                var from1 = GetNodeIndex(literal1.Negate());
                var to1 = GetNodeIndex(literal2);

                graph.AddEdge(from1, to1);

                var from2 = GetNodeIndex(literal2.Negate());
                var to2 = GetNodeIndex(literal1);

                graph.AddEdge(from2, to2);
            }

            var calculator = new StronglyConnectedComponentsCalculator<int>(graph);
            var components = calculator.GetStronglyConnectedComponents().ToArray();
            foreach (var component in components)
            {
                var positiveLiteralNodes = component.Where(node => node >= 0).ToArray();
                var negativeLiteralNodes = new HashSet<int>(component.Where(node => node < 0));
                if (positiveLiteralNodes.Any(node => negativeLiteralNodes.Contains(GetNegativeLiteralNodeIndex(node))))
                {
                    return false;
                }
            }

            return true;
        }

        private static int GetNodeIndex(Literal literal)
        {
            return literal.Negation ? GetNegativeLiteralNodeIndex(literal.Index) : literal.Index;
        }

        private static int GetNegativeLiteralNodeIndex(int index)
        {
            return -index;
        }
    }
}
