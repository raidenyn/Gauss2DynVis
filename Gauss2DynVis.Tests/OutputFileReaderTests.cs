using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Gauss2DynVis.Tests
{
    public class OutputFileReaderTests
    {
        [Test]
        public void ReadTestFile()
        {
            var stringReader = new StringReader(OutputFiles.FCH3Cl);

            var iterations = new OutputFileReader().ReadFile(stringReader);

            Assert.Greater(iterations.Count, 0);
            Trace.WriteLine(string.Format("Interation count: {0}", iterations.Count));

            foreach (var optIteration in iterations)
            {
                Assert.IsNotNull(optIteration.StandardOrientation);
                Assert.IsNotNull(optIteration.ZMatrixOrientation);
                Assert.Greater(optIteration.StandardOrientation.Count, 0);
                Assert.Greater(optIteration.ZMatrixOrientation.Count, 0);
                //Assert.AreNotEqual(0, optIteration.Energy);
            }

            var optimals = iterations.Where(iter => iter.IsOptimal).ToList();

            using (var file = File.OpenWrite("test.sam"))
            {
                new SamFileCreater(optimals, new Parameters { Q1Count = 11, Q2Count = 11 }).WriteTo(file);
            }
            using (var file = File.OpenWrite("test.srs"))
            {
                new SrsFileCreater(optimals, new Parameters { Q1Count = 11, Q2Count = 11 }).WriteTo(file);
            }

            
            
        }
    }
}
