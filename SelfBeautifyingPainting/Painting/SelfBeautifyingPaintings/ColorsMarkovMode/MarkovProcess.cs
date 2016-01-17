using System.Collections.Generic;
using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorProbabilityMode;

namespace SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorsMarkovMode
{
    public struct MarkovProcess
    {
        public List<ColorProbabilityPair> InitialCDFs;
        public double ProbabilityOfRemainingTheSame;
    }
}