using System;
using System.Collections.Generic;
using System.Drawing;
using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorProbabilityMode;

namespace SelfBeautifyingPainting.Helpers
{
    internal static class ValueProbabilityPairGeneration
    {
        public static List<ValueProbabilityPair<TValue>> GenerateValueCDFPairs<TValue>(
            Func<TValue> valuesCreatingFunction)
        {
            var colorProbabilityPairs = new List<ValueProbabilityPair<TValue>>();

            var probabilityLeft = 1.0;


            for (; probabilityLeft >= 0.01;)
            {
                var colorProbabilityPair = new ValueProbabilityPair<TValue>
                {
                    Value = valuesCreatingFunction(),
                    Probability = probabilityLeft*RandomProvider.RandomGenerator.NextDouble()
                };

                colorProbabilityPairs.Add(colorProbabilityPair);
                probabilityLeft -= colorProbabilityPair.Probability;
            }

            colorProbabilityPairs.Add(new ValueProbabilityPair<TValue>
            {
                Value = valuesCreatingFunction(),
                Probability = probabilityLeft
            });

            var colorCDFPairs = new List<ValueProbabilityPair<TValue>>(colorProbabilityPairs);
            colorCDFPairs.Sort((cp1, cp2) => cp1.Probability.CompareTo(cp2.Probability));

            for (var i = 1; i < colorCDFPairs.Count; i++)
            {
                colorCDFPairs[i] = new ValueProbabilityPair<TValue>
                {
                    Value = colorCDFPairs[i].Value,
                    Probability = colorCDFPairs[i].Probability + colorCDFPairs[i - 1].Probability
                };
            }
            return colorCDFPairs;
        }

        public static List<ColorProbabilityPair> GenerateColorCDFPairs()
        {
            var colorProbabilityPairs = new List<ColorProbabilityPair>();

            var probabilityLeft = 1.0;


            for (; probabilityLeft >= 0.01;)
            {
                var colorProbabilityPair = new ColorProbabilityPair
                {
                    color = Color.FromArgb(255,
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
                color = Color.FromArgb(255,
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
    }
}