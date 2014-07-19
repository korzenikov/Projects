using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class ClusteringBigTask  : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("InputFiles//clustering_big.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                reader.ReadLine();
                while (true)
                {
                    string row = reader.ReadLine();
                    if (row == null)
                    {
                        break;
                    }

                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var bits = parts.Select(x => int.Parse(x, CultureInfo.InvariantCulture)).ToArray();
                }
            }
        }
    }
}
