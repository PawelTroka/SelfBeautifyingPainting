using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SelfBeautifyingPainting.Helpers;
using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorProbabilityMode;

namespace SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ShapeMode
{
    public class ShapeDrawer
    {
        private readonly Shape[] AllShapes = Enum.GetValues(typeof (Shape)).Cast<Shape>().ToArray();
        private readonly List<ColorProbabilityPair> colorsCDFs;

        private readonly List<ValueProbabilityPair<byte>> ProbabilitiesOfShapeCount;
        private readonly List<ValueProbabilityPair<Shape>> ProbabilitiesOfShapes;

        public ShapeDrawer()
        {
            colorsCDFs = ValueProbabilityPairGeneration.GenerateColorCDFPairs();
            ProbabilitiesOfShapes =
                ValueProbabilityPairGeneration.GenerateValueCDFPairs(
                    () => { return AllShapes[RandomProvider.RandomGenerator.Next(0, AllShapes.Length)]; });

            ProbabilitiesOfShapeCount =
                ValueProbabilityPairGeneration.GenerateValueCDFPairs(
                    () => { return (byte) RandomProvider.RandomGenerator.Next(1, 255); });
        }


        public ShapeDrawer(ShapeDrawer shapeDrawer)
        {
            colorsCDFs = new List<ColorProbabilityPair>(shapeDrawer.colorsCDFs);
            ProbabilitiesOfShapes = new List<ValueProbabilityPair<Shape>>(shapeDrawer.ProbabilitiesOfShapes);
            ProbabilitiesOfShapeCount = new List<ValueProbabilityPair<byte>>(shapeDrawer.ProbabilitiesOfShapeCount);
        }


        public void DrawMany(Graphics g, int xmin, int xmax, int ymin, int ymax)
        {
            var randomValue = RandomProvider.RandomGenerator.NextDouble();
            var countOfDrawing = 0;
            foreach (var valueProbabilityPair in ProbabilitiesOfShapeCount)
            {
                if (randomValue < valueProbabilityPair.Probability)
                {
                    countOfDrawing = valueProbabilityPair.Value;
                    break;
                }
            }

            for (var i = 0; i < countOfDrawing; i++)
            {
                DrawOne(g, xmin, xmax, ymin, ymax);
            }
        }

        private void DrawOne(Graphics g, int xmin, int xmax, int ymin, int ymax)
        {
            var shape = Shape.Arc;
            var color = new Color();
            var randomValue = RandomProvider.RandomGenerator.NextDouble();
            foreach (var probabilitiesOfShape in ProbabilitiesOfShapes)
            {
                if (randomValue < probabilitiesOfShape.Probability)
                {
                    shape = probabilitiesOfShape.Value;
                    break;
                }
            }

            randomValue = RandomProvider.RandomGenerator.NextDouble();

            foreach (var colorProbabilityPair in colorsCDFs)
            {
                if (randomValue < colorProbabilityPair.probability)
                {
                    color = colorProbabilityPair.color;
                    break;
                }
            }

            var pen = new Pen(color);

            var x1 = RandomProvider.RandomGenerator.Next(xmin, xmax + 1);
            var y1 = RandomProvider.RandomGenerator.Next(ymin, ymax + 1);

            var x2 = RandomProvider.RandomGenerator.Next(xmin, xmax + 1);
            var y2 = RandomProvider.RandomGenerator.Next(ymin, ymax + 1);

            var x3 = RandomProvider.RandomGenerator.Next(xmin, xmax + 1);
            var y3 = RandomProvider.RandomGenerator.Next(ymin, ymax + 1);

            var x4 = RandomProvider.RandomGenerator.Next(xmin, xmax + 1);
            var y4 = RandomProvider.RandomGenerator.Next(ymin, ymax + 1);

            var w1 = RandomProvider.RandomGenerator.Next(x1, xmax + 1);
            var h1 = RandomProvider.RandomGenerator.Next(y1, ymax + 1);

            var angle1 = RandomProvider.RandomGenerator.Next(0, 360);

            switch (shape)
            {
                case Shape.Arc:
                    g.DrawArc(pen, x1, y1, w1, h1, RandomProvider.RandomGenerator.Next(0, 360),
                        RandomProvider.RandomGenerator.Next(0, 360));
                    break;
                case Shape.Bezier:
                    g.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
                    break;
                case Shape.ClosedCurve:
                    g.DrawClosedCurve(pen,
                        new[] {new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3), new PointF(x4, y4)});
                    break;
                case Shape.Curve:
                    g.DrawCurve(pen,
                        new[] {new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3), new PointF(x4, y4)});
                    break;
                case Shape.Ellipse:
                    g.DrawEllipse(pen, x1, y1, w1, h1);
                    break;
                case Shape.Line:
                    g.DrawLine(pen, x1, y1, x2, y2);
                    break;
                case Shape.Lines:
                    g.DrawLines(pen,
                        new[] {new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3), new PointF(x4, y4)});
                    break;
                case Shape.Pie:
                    g.DrawPie(pen, x1, y1, w1, h1, RandomProvider.RandomGenerator.Next(0, 360),
                        RandomProvider.RandomGenerator.Next(0, 360));
                    break;
                case Shape.Polygon:
                    g.DrawPolygon(pen,
                        new[] {new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3), new PointF(x4, y4)});
                    break;
                case Shape.Rectangle:
                    g.DrawRectangle(pen, x1, y1, w1, h1);
                    break;
                case Shape.String:
                    g.DrawString(EnglishWordsDictionary.GetRandomWord(),
                        new Font("Cambria", RandomProvider.RandomGenerator.Next(1, 50)), new SolidBrush(color),
                        new PointF(x1, y1));
                    break;
                case Shape.FillClosedCurve:
                    g.FillClosedCurve(new SolidBrush(color),
                        new[] {new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3), new PointF(x4, y4)});
                    break;
                case Shape.FillEllipse:
                    g.FillEllipse(new SolidBrush(color), x1, y1, w1, h1);
                    break;
                case Shape.FillPie:
                    g.FillPie(new SolidBrush(color), x1, y1, w1, h1, RandomProvider.RandomGenerator.Next(0, 360),
                        RandomProvider.RandomGenerator.Next(0, 360));
                    break;
                case Shape.FillPolygon:
                    g.FillPolygon(new SolidBrush(color),
                        new[] {new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3), new PointF(x4, y4)});
                    break;
                case Shape.FillRectangle:
                    g.FillRectangle(new SolidBrush(color), x1, y1, w1, h1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // g.Save();
        }
    }
}