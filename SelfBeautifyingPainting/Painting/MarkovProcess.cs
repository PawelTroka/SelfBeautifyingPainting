using System.Collections.Generic;

namespace SelfBeautifyingPainting.Painting
{
    public struct MarkovProcess
    {
        public List<ColorProbabilityPair> InitialCDFs;
        public double ProbabilityOfRemainingTheSame;
    }
}