using System;
using System.Collections.Generic;
using System.Linq;
using SelfBeautifyingPainting.Helpers;

namespace SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorProbabilityMode
{
    internal class ColorsWithProbabilitySelfBeautifyingPainting : SelfBeautifyingPainting
    {
        private readonly Dictionary<PaintingFragment, List<ColorProbabilityPair>> colorsWithProbability;


        public ColorsWithProbabilitySelfBeautifyingPainting(int w, int h) : base(w, h)
        {
            colorsWithProbability = new Dictionary<PaintingFragment, List<ColorProbabilityPair>>();
            foreach (var value in Enum.GetValues(typeof (PaintingFragment)))
            {
                colorsWithProbability[(PaintingFragment) value] = ValueProbabilityPairGeneration.GenerateColorCDFPairs();
            }

            updateFunction = (fragment, x, y) =>
            {
                var randomValue = RandomProvider.RandomGenerator.NextDouble();

                for (var i = 0; i < colorsWithProbability[fragment].Count; i++)
                {
                    if (randomValue < colorsWithProbability[fragment][i].probability)
                        return colorsWithProbability[fragment][i].color;
                }
                return colorsWithProbability[fragment].Last().color;
            };
            UpdatePainting();
        }

        //    public override event EventHandler ImageChanged;

        protected override void ChangeNotLikedFragmentToLiked(PaintingFragment fragment)
        {
            if (fragmentLiked != null)
            {
                colorsWithProbability[fragment] =
                    new List<ColorProbabilityPair>(colorsWithProbability[fragmentLiked.Value]);
            }
        }

        protected override void ChangHatedFragment()
        {
            //    if (Mode == PaintingMode.GoogleTopicsImages)
            {
                if (fragmentHated != null)
                {
                    colorsWithProbability[fragmentHated.Value] = ValueProbabilityPairGeneration.GenerateColorCDFPairs();
                }
            }
        }
    }
}