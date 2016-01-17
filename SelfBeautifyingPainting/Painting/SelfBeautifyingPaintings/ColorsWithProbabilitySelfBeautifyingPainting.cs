using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SelfBeautifyingPainting.Helpers;

namespace SelfBeautifyingPainting.Painting
{
    internal class ColorsWithProbabilitySelfBeautifyingPainting : SelfBeautifyingPainting
    {
        private readonly Dictionary<PaintingFragment, List<ColorProbabilityPair>> colorsWithProbability;


        public ColorsWithProbabilitySelfBeautifyingPainting(int w, int h) : base(w, h)
        {
            colorsWithProbability = new Dictionary<PaintingFragment, List<ColorProbabilityPair>>();
            foreach (var value in Enum.GetValues(typeof (PaintingFragment)))
            {
                colorsWithProbability[(PaintingFragment) value] = GenerateColorCDFPairs();
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

        public static List<ColorProbabilityPair> GenerateColorCDFPairs()
        {
            var colorProbabilityPairs = new List<ColorProbabilityPair>();

            var probabilityLeft = 1.0;


            for (; probabilityLeft >= 0.01;)
            {
                var colorProbabilityPair = new ColorProbabilityPair
                {
                    color = Color.FromArgb((byte) RandomProvider.RandomGenerator.Next(0, 255),
                        (byte) RandomProvider.RandomGenerator.Next(0, 255),
                        (byte) RandomProvider.RandomGenerator.Next(0, 255),
                        (byte) RandomProvider.RandomGenerator.Next(0, 255)),
                    probability = probabilityLeft*RandomProvider.RandomGenerator.NextDouble()
                };

                colorProbabilityPairs.Add(colorProbabilityPair);
                probabilityLeft -= colorProbabilityPair.probability;
            }

            colorProbabilityPairs.Add(new ColorProbabilityPair
            {
                color = Color.FromArgb((byte) RandomProvider.RandomGenerator.Next(0, 255),
                    (byte) RandomProvider.RandomGenerator.Next(0, 255),
                    (byte) RandomProvider.RandomGenerator.Next(0, 255),
                    (byte) RandomProvider.RandomGenerator.Next(0, 255)),
                probability = probabilityLeft
            });

            var colorCDFPairs = new List<ColorProbabilityPair>(colorProbabilityPairs);
            colorCDFPairs.Sort((cp1, cp2) => cp1.probability.CompareTo(cp2.probability));

            for (var i = 1; i < colorCDFPairs.Count; i++)
            {
                colorCDFPairs[i] = new ColorProbabilityPair
                {
                    color = colorCDFPairs[i].color,
                    probability = colorCDFPairs[i].probability + colorCDFPairs[i - 1].probability
                };
            }
            return colorCDFPairs;
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
                    colorsWithProbability[fragmentHated.Value] = GenerateColorCDFPairs();
                }
            }
        }
    }
}