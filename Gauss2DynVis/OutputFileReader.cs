using System.Collections.Generic;
using System.IO;

namespace Gauss2DynVis
{
    public class OutputFileReader
    {
        private readonly static List<IPattern> Patterns = new List<IPattern>
            {
                new EnergyPattern(),
                new OptimizationComplitedPattern(),
                new StandardOrientationPattern(),
                new ZMatrixOrientationPattern(),
                new InputOrientationPattern()
            };

        private readonly static NewIterationPattern NewIterationPattern = new NewIterationPattern();

        public List<OptIteration> ReadFile(TextReader reader)
        {
            var list = new OptIterationList();

            do
            {
                string line = reader.ReadLine();

                if (line == null)
                {
                    break;
                }

                if (NewIterationPattern.IsNewIteration(line))
                {
                    list.Next();
                }

                foreach (var pattern in Patterns)
                {
                    if (pattern.Parse(line, reader, list.Current))
                    {
                        break;
                    }
                }

            } while (true);

            return list.OptIterations;
        }

        public List<OptIteration> ReadFile(Stream file)
        {
            var reader = new StreamReader(file);
            return ReadFile(reader);
        }
    }
}
