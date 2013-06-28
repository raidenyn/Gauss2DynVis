using NUnit.Framework;

namespace Gauss2DynVis.Tests
{
    public class AtomCoordTests
    {
        [Test]
        public void AtomCoordFromString()
        {
            var atomCoord = AtomCoord.CreateFormString("0 5 -1 10 11.6546000   12.98565789");

            Assert.IsNotNull(atomCoord);

            Assert.AreEqual(5, atomCoord.Element);
            Assert.AreEqual(10d, atomCoord.Coord.X);
            Assert.AreEqual(11.6546d, atomCoord.Coord.Y);
            Assert.AreEqual(12.98565789d, atomCoord.Coord.Z);
        }

        [Test]
        public void AtomCoordToString()
        {
            var atomCoord = new AtomCoord();

            atomCoord.Element = 5;
            atomCoord.Coord = new Point3D(10, 11.55, 12.65);

            Assert.AreEqual("5        10.000000000    11.550000000    12.650000000", atomCoord.ToString());
        }
    }
}
