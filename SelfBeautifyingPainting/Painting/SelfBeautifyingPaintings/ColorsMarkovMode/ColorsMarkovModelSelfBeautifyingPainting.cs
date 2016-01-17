using System;
using System.Collections.Generic;
using System.Drawing;
using SelfBeautifyingPainting.Helpers;
using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorProbabilityMode;

namespace SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorsMarkovMode
{
    internal class ColorsMarkovModelSelfBeautifyingPainting : SelfBeautifyingPainting
    {
        private readonly Color[,] _colors;
        private readonly Dictionary<PaintingFragment, MarkovProcess> colorsWithMarkovModel;


        public ColorsMarkovModelSelfBeautifyingPainting(int w, int h) : base(w, h)
        {
            smoothingEnabled = false;
            _colors = new Color[w, h];
            //colorsWithProbability = new Dictionary<PaintingFragment, List<ColorProbabilityPair>>();
            colorsWithMarkovModel = new Dictionary<PaintingFragment, MarkovProcess>();
            foreach (var value in Enum.GetValues(typeof (PaintingFragment)))
            {
                colorsWithMarkovModel[(PaintingFragment) value] = new MarkovProcess
                {
                    InitialCDFs = ValueProbabilityPairGeneration.GenerateColorCDFPairs(),
                    ProbabilityOfRemainingTheSame = RandomProvider.RandomGenerator.NextDouble()
                };
            }

            updateFunction = (fragment, xa, ya) =>
            {
                var x = xa; // + PaintingFragmentToCoordsOffset(fragment).X;
                var y = ya; // + PaintingFragmentToCoordsOffset(fragment).Y;
                var randomValue = RandomProvider.RandomGenerator.NextDouble();

                if (x > 0 && y > 0 && randomValue < colorsWithMarkovModel[fragment].ProbabilityOfRemainingTheSame)
                {
                    _colors[x, y] =
                        _colors[
                            x - RandomProvider.RandomGenerator.Next(0, 2), y - RandomProvider.RandomGenerator.Next(0, 2)
                            ];
                    //getDominantColor(_colors[x - 1, y], _colors[x, y - 1]);//_colors[x - RandomProvider.RandomGenerator.Next(0,2), y - RandomProvider.RandomGenerator.Next(0, 2)];
                    // return _colors[x, y];
                }
                else if (x > 0 && y == 0 && randomValue < colorsWithMarkovModel[fragment].ProbabilityOfRemainingTheSame)
                {
                    _colors[x, y] = _colors[x - 1, y];
                    //return _colors[x, y];
                }
                else if (x == 0 && y > 0 && randomValue < colorsWithMarkovModel[fragment].ProbabilityOfRemainingTheSame)
                {
                    _colors[x, y] = _colors[x, y - 1];
                }
                else
                {
                    randomValue = RandomProvider.RandomGenerator.NextDouble();
                    for (var i = 0; i < colorsWithMarkovModel[fragment].InitialCDFs.Count; i++)
                    {
                        if (randomValue < colorsWithMarkovModel[fragment].InitialCDFs[i].probability)
                            _colors[x, y] = colorsWithMarkovModel[fragment].InitialCDFs[i].color;
                    }
                }

                return _colors[x, y];
            };

            UpdatePainting();
        }

        //    public override event EventHandler ImageChanged;


        private static Color getDominantColor(params Color[] bmp)
        {
            var colorsCount = new Dictionary<Color, int>();

            foreach (var color in bmp)
            {
                if (colorsCount.ContainsKey(color))
                    colorsCount[color]++;
                else
                    colorsCount[color] = 1;
            }

            var dominantColor = Color.Transparent;
            var count = 0;

            foreach (var i in colorsCount)
            {
                if (i.Value > count)
                {
                    count = i.Value;
                    dominantColor = i.Key;
                }
            }

            return dominantColor;

            //Used for tally
            var r = 0;
            var g = 0;
            var b = 0;

            var total = 0;

            for (var x = 0; x < bmp.Length; x++)
            {
                //  for (int y = 0; y < bmp.Height; y++)
                {
                    var clr = bmp[x];

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;

                    total++;
                }
            }

            //Calculate average
            r /= total;
            g /= total;
            b /= total;

            return Color.FromArgb(r, g, b);
        }

        protected override void ChangeNotLikedFragmentToLiked(PaintingFragment fragment)
        {
            if (fragmentLiked != null)
            {
                colorsWithMarkovModel[fragment] = new MarkovProcess
                {
                    InitialCDFs = new List<ColorProbabilityPair>(colorsWithMarkovModel[fragmentLiked.Value].InitialCDFs),
                    ProbabilityOfRemainingTheSame =
                        colorsWithMarkovModel[fragmentLiked.Value].ProbabilityOfRemainingTheSame
                };
            }
        }

        protected override void ChangHatedFragment()
        {
            //    if (Mode == PaintingMode.GoogleTopicsImages)
            {
                if (fragmentHated != null)
                {
                    colorsWithMarkovModel[fragmentHated.Value] = new MarkovProcess
                    {
                        InitialCDFs = ValueProbabilityPairGeneration.GenerateColorCDFPairs(),
                        ProbabilityOfRemainingTheSame = RandomProvider.RandomGenerator.NextDouble()
                    };
                }
            }
        }
    }
}