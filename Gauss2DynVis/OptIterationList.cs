using System.Collections.Generic;
using System.Linq;

namespace Gauss2DynVis
{
    class OptIterationList
    {
        public OptIterationList()
        {
            OptIterations = new List<OptIteration>();
        }

        public List<OptIteration> OptIterations { get; private set; }
 
        public OptIteration Current{get { return OptIterations.LastOrDefault(); }}

        public void Next()
        {
            OptIterations.Add(new OptIteration());
        }
    }
}