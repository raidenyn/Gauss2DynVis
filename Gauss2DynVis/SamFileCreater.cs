using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gauss2DynVis
{
    public class SamFileCreater
    {
        private readonly IEnumerable<OptIteration> _iterations;
        private readonly Parameters _param;

        public SamFileCreater(IEnumerable<OptIteration> iterations, Parameters param)
        {
            _iterations = iterations;
            _param = param;
        }


        public void WriteTo(Stream stream)
        {
            if (_iterations.Count() != _param.Q1Count*_param.Q2Count)
            {
                throw new ArgumentException("Iterations count is not equeal Q1Count * Q2Count");
            }

            var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true);

            writer.WriteLine("Axes1: 'Axes1' [normal Angstrom] (Ended,Ended)");
            writer.WriteLine("Axes2: 'Axes2' [normal Angstrom] (Ended,Ended)");
            writer.WriteLine();
            writer.WriteLine("Q1 Values:");
            writer.WriteLine("<Тут координаты через пробел>");
            writer.WriteLine();
            writer.WriteLine("Q2 Values:");
            writer.WriteLine("<Тут координаты через пробел>");
            writer.WriteLine();
            writer.WriteLine("Energy Values:");

            foreach (var iter in _iterations)
            {
                writer.WriteLine(iter.Energy.ToString());
            }

            writer.Close();
        }
    }
}
