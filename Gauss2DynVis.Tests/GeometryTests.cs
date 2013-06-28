using NUnit.Framework;

namespace Gauss2DynVis.Tests
{
    public class GeometryTests
    {
        [Test]
        public void GeometryFromString()
        {
            var geometry = Geometry.CreateFormString(
@"1 5 0 10 11.6546000   12.98565789
  2 6 0 14 15.6546000   16.98565789
  3 7 0 17 18.6546000   20.98565789");

            Assert.IsNotNull(geometry);
            Assert.AreEqual(3, geometry.Count);

            Assert.AreEqual(5, geometry[0].Element);
            Assert.AreEqual(6, geometry[1].Element);
            Assert.AreEqual(7, geometry[2].Element);
            Assert.AreEqual(10d, geometry[0].Coord.X);
            Assert.AreEqual(11.6546d, geometry[0].Coord.Y);
            Assert.AreEqual(12.98565789d, geometry[0].Coord.Z);
            Assert.AreEqual(14d, geometry[1].Coord.X);
            Assert.AreEqual(15.6546d, geometry[1].Coord.Y);
            Assert.AreEqual(16.98565789d, geometry[1].Coord.Z);
            Assert.AreEqual(17d, geometry[2].Coord.X);
            Assert.AreEqual(18.6546d, geometry[2].Coord.Y);
            Assert.AreEqual(20.98565789d, geometry[2].Coord.Z);
        }

        [Test]
        public void GeometryToString()
        {
            var geometry = new Geometry
                {
                    new AtomCoord {Element = 5, Coord = new Point3D(10, 11.55, 12.65)},
                    new AtomCoord {Element = 6, Coord = new Point3D(14, 15.55, 16.65)},
                    new AtomCoord {Element = 7, Coord = new Point3D(17, 19.55, 20.65)}
                };



            Assert.AreEqual(
@"5        10.000000000    11.550000000    12.650000000
6        14.000000000    15.550000000    16.650000000
7        17.000000000    19.550000000    20.650000000
",
geometry.ToString());
        }
    }
}
