using System;
using System.Text.RegularExpressions;

namespace Gauss2DynVis
{
    public class AtomCoord
    {
        public int Element { get; set; }
        public Point3D Coord { get; set; }

        private static readonly Regex LinePattern = new Regex(@"(?<center>-?\d+)\s+(?<element>-?\d+)\s+(?<type>-?\d+)\s+(?<x>-?\d+(\.\d+)?)\s+(?<y>-?\d+(\.\d+)?)\s+(?<z>-?\d+(\.\d+)?)");

        public static AtomCoord CreateFormString(string str)
        {
            var result = new AtomCoord();
            if (result.SetFromString(str))
            {
                return result;
            }
            return null;
        }

        public bool SetFromString(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            var atomCoordMatch = LinePattern.Match(str);

            if (!atomCoordMatch.Success)
            {
                return false;
            }

            Element = int.Parse(atomCoordMatch.Groups["element"].Value);

            double x = double.Parse(atomCoordMatch.Groups["x"].Value);
            double y = double.Parse(atomCoordMatch.Groups["y"].Value);
            double z = double.Parse(atomCoordMatch.Groups["z"].Value);

            Coord = new Point3D(x, y, z);

            return true;
        }

        public override string ToString()
        {
            return String.Format("{0,-5} {1,15:F9} {2,15:F9} {3,15:F9}", Element, Coord.X, Coord.Y, Coord.Z);
        }
    }
}
