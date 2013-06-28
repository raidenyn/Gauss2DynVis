using System.IO;
using System.Text.RegularExpressions;

namespace Gauss2DynVis
{
    public interface IPattern
    {
        bool Parse(string line, TextReader file, OptIteration iteration);
    }

    public class NewIterationPattern
    {
        private readonly static Regex NewIterationRegex = new Regex(@"(Z-MATRIX \(ANGSTROMS AND DEGREES\))|(Input orientation:)");

        public bool IsNewIteration(string line)
        {
            var match = NewIterationRegex.Match(line);

            if (match.Success)
            {
                return true;
            }

            return false;
        }
    }

    public class EnergyPattern : IPattern
    {
        private readonly static Regex EnergyRegex = new Regex(@"SCF Done: *E\(.*\) *= *(?<energy>[-\.\d]+)");

        public bool Parse(string line, TextReader file, OptIteration iteration)
        {
            var match = EnergyRegex.Match(line);

            if (match.Success)
            {
                iteration.Energy = decimal.Parse(match.Groups["energy"].Value);
                return true;
            }

            return false;
        }
    }

    public class OptimizationComplitedPattern : IPattern
    {
        private readonly static Regex OptCompliteRegex = new Regex(@"Optimization completed");
        private readonly static Regex OptNotCompliteRegex = new Regex(@"Predicted change in Energy");

        public bool Parse(string line, TextReader file, OptIteration iteration)
        {
            var match = OptCompliteRegex.Match(line);

            if (match.Success)
            {
                iteration.IsOptimal = true;
                return true;
            }

            return false;
        }
    }

    public abstract class OrientationPattern : IPattern
    {
        protected abstract Regex OrientationNameRegex { get; }

        protected Geometry GetGeom(string line, TextReader file)
        {
            var match = OrientationNameRegex.Match(line);

            if (match.Success)
            {
                file.SkipLines(4);

                return Geometry.CreateFormText(file);
            }

            return null;
        }

        public abstract bool Parse(string line, TextReader file, OptIteration iteration);
    }


    public class StandardOrientationPattern : OrientationPattern
    {
        private readonly static Regex StandardOrientationRegex = new Regex(@"Standard orientation:");

        protected override Regex OrientationNameRegex
        {
            get { return StandardOrientationRegex; }
        }

        public override bool Parse(string line, TextReader file, OptIteration iteration)
        {
            var geom = GetGeom(line, file);

            if (geom != null)
            {
                iteration.StandardOrientation = geom;
            }

            return geom != null;
        }
    }

    public class ZMatrixOrientationPattern : OrientationPattern
    {
        private readonly static Regex StandardOrientationRegex = new Regex(@"Z-Matrix orientation:");

        protected override Regex OrientationNameRegex
        {
            get { return StandardOrientationRegex; }
        }

        public override bool Parse(string line, TextReader file, OptIteration iteration)
        {
            var geom = GetGeom(line, file);

            if (geom != null)
            {
                iteration.ZMatrixOrientation = geom;
            }

            return geom != null;
        }
    }

    public class InputOrientationPattern : OrientationPattern
    {
        private readonly static Regex StandardOrientationRegex = new Regex(@"Input orientation:");

        protected override Regex OrientationNameRegex
        {
            get { return StandardOrientationRegex; }
        }

        public override bool Parse(string line, TextReader file, OptIteration iteration)
        {
            var geom = GetGeom(line, file);

            if (geom != null)
            {
                iteration.InputOrientation = geom;
            }

            return geom != null;
        }
    }
}
