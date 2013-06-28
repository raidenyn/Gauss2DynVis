using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gauss2DynVis
{
    public class SrsFileCreater
    {
        private readonly IEnumerable<OptIteration> _iterations;
        private readonly Parameters _param;

        public SrsFileCreater(IEnumerable<OptIteration> iterations, Parameters param)
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

            var geoms = _iterations.Select(iter => new GeomSource(iter)).ToList();
            var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true);

            writer.WriteLine();
            writer.WriteLine("Elements:");

            foreach (var atom in geoms.First().Geom)
            {
                writer.Write(String.Format("  {0}", atom.Element)); 
            }

            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine("Structures:");

            foreach (var geom in geoms)
            {
                writer.WriteLine(geom.Geom.ToString());
            }

            writer.Close();
        }

        class GeomSource
        {
            private readonly OptIteration _iteration;

            public GeomSource(OptIteration iteration)
            {
                _iteration = iteration;
            }

            public Geometry Geom { get { return _iteration.StandardOrientation; } }
        }
    }

    public class Parameters
    {
        public int Q1Count { get; set; }
        public int Q2Count { get; set; }

        public bool Q1Inverse { get; set; }
        public bool Q2Inverse { get; set; }
    }
}
