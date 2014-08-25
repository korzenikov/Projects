using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class TwoSatSolverTask : ITask
    {
        public void Run()
        {
            using (var writer = new StreamWriter("output.txt"))
            {
                CheckSample("InputFiles//2sat1.txt", writer);
                CheckSample("InputFiles//2sat2.txt", writer);
                CheckSample("InputFiles//2sat3.txt", writer);
                CheckSample("InputFiles//2sat4.txt", writer);
                CheckSample("InputFiles//2sat5.txt", writer);
                CheckSample("InputFiles//2sat6.txt", writer);
            }
        }

        private IEnumerable<Clause> GetClauses(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            var clauses = new List<Clause>();
            reader.ReadLine();
            while (true)
            {
                string row = reader.ReadLine();
                if (row == null)
                {
                    break;
                }

                var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                var numbers = parts.Select(x => int.Parse(x, CultureInfo.InvariantCulture)).ToArray();
                var number1 = numbers[0];
                var number2 = numbers[1];

                var literal1 = number1 < 0 ? new Literal(-number1).Negate() : new Literal(number1);

                var literal2 = number2 < 0 ? new Literal(-number2).Negate() : new Literal(number2);

                var clause = new Clause(literal1, literal2);
                clauses.Add(clause);
            }

            return clauses;
        }

        private void CheckSample(string inputfile, StreamWriter writer)
        {
            using (var reader = new StreamReader(inputfile))
            {
                var clauses = GetClauses(reader).ToArray();
                var isSatisfiable = TwoSatSolver.IsSatisfiable(clauses);
                writer.Write(isSatisfiable ? "1" : "0");
            }
        }
    }
}
