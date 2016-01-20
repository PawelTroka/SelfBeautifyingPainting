using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging.Filters;
using SelfBeautifyingPainting.Helpers;

namespace SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings
{
    internal abstract class SelfBeautifyingPainting
    {

        protected readonly List<PaintingFragment> fragmentsToUpdate;
        private readonly int height = 1080;
        private readonly KnownColor[] knownColors = (KnownColor[]) Enum.GetValues(typeof (KnownColor));
        private readonly int width = 1920;
        protected PaintingFragment? fragmentHated;
        protected PaintingFragment? fragmentLiked;

        protected bool smoothingEnabled = true;
        //protected readonly Random random = new Random();
        protected Func<PaintingFragment, int, int, Color> updateFunction;

        protected SelfBeautifyingPainting(int w, int h)
        {
            height = h;
            width = w;
            //  googleTopicsImageFinder = new GoogleTopicsImageFinder(w, h);
            //  colors = new Dictionary<PaintingFragment, Range>();
            //  topics = new Dictionary<PaintingFragment, string>();

            //  images = new Dictionary<PaintingFragment, Bitmap>();
            fragmentsToUpdate = new List<PaintingFragment>();


            foreach (var value in Enum.GetValues(typeof (PaintingFragment)))
            {
                fragmentsToUpdate.Add((PaintingFragment) value);
                var randomByte1 = (byte) RandomProvider.RandomGenerator.Next(knownColors.Length);
                var randomByte2 = (byte) RandomProvider.RandomGenerator.Next(randomByte1, knownColors.Length);

                //    colors[(PaintingFragment)value] = new Range(randomByte1, randomByte2);
                //   topics[(PaintingFragment)value] = EnglishWordsDictionary.GetRandomWord();
                //  images[(PaintingFragment)value] = googleTopicsImageFinder.GetPicture(EnglishWordsDictionary.GetRandomWord());
            }


            Painting = new Bitmap(width, height);
            // UpdatePainting();
        }

        // public PaintingMode Mode { get; set; }
        public Bitmap Painting { get; }

        protected PaintingFragment CoordsToPaintingFragment(int x, int y)
        {
            if (x <= width/2 && y <= height/2)
                return PaintingFragment.LeftTop;

            if (x > width/2 && y > height/2)
                return PaintingFragment.RightBottom;

            if (x > width/2 && y <= height/2)
                return PaintingFragment.RightTop;

            if (x <= width/2 && y > height/2)
                return PaintingFragment.LeftBottom;

            return PaintingFragment.LeftTop;
        }

        protected Coords PaintingFragmentToCoordsOffset(PaintingFragment pf)
        {
            switch (pf)
            {
                case PaintingFragment.LeftTop:
                    return new Coords(0, 0);
                case PaintingFragment.RightTop:
                    return new Coords(width/2, 0);
                case PaintingFragment.LeftBottom:
                    return new Coords(0, height/2);
                case PaintingFragment.RightBottom:
                    return new Coords(width/2, height/2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(pf), pf, null);
            }
        }

        public void RepaintAll()
        {
            fragmentHated = fragmentLiked = null;
            foreach (PaintingFragment value in Enum.GetValues(typeof (PaintingFragment)))
                clearFragment(value);
            UpdatePainting();
        }

        protected CoordsRange PaintingFragmentToRange(PaintingFragment pf)
        {
            switch (pf)
            {
                case PaintingFragment.LeftTop:
                    return new CoordsRange(new Coords(0, 0), new Coords(width/2, height/2));
                case PaintingFragment.RightTop:
                    return new CoordsRange(new Coords(width/2, 0), new Coords(width, height/2));
                case PaintingFragment.LeftBottom:
                    return new CoordsRange(new Coords(0, height/2), new Coords(width/2, height));
                case PaintingFragment.RightBottom:
                    return new CoordsRange(new Coords(width/2, height/2), new Coords(width, height));
                default:
                    throw new ArgumentOutOfRangeException(nameof(pf), pf, null);
            }
        }

        protected void clearFragment(PaintingFragment pf)
        {
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    if (CoordsToPaintingFragment(x, y) == pf)
                    {
                        Painting.SetPixel(x, y, Color.White);
                    }
                }
        }

        public event EventHandler ImageChanged;


        protected bool DoesPaintingFragmentNeedRepaint(PaintingFragment pf)
        {
            return (!fragmentLiked.HasValue && !fragmentHated.HasValue) ||
                   (fragmentLiked.HasValue && pf != fragmentLiked.Value) ||
                   (fragmentHated.HasValue && pf == fragmentHated.Value);
        }

        protected virtual void UpdatePainting()
        {
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    var pf = CoordsToPaintingFragment(x, y);

                    if (DoesPaintingFragmentNeedRepaint(pf))
                    {
                        var coordsOffset = PaintingFragmentToCoordsOffset(pf);

                        Painting.SetPixel(x, y, updateFunction(pf, x - coordsOffset.X, y - coordsOffset.Y));

                        /*updateFunction
                        switch (Mode)
                        {


                            case PaintingMode.GoogleImagesRelated:
                            case PaintingMode.GoogleTopicsImages:

                                //if(x - coordsOffset.X < images[pf].Width &&  y - coordsOffset.Y< images[pf].Height)
                                Painting.SetPixel(x, y, images[pf].GetPixel(x - coordsOffset.X, y - coordsOffset.Y));
                                break;
                            case PaintingMode.Colors:

                                Painting.SetPixel(x, y,
                                    Color.FromKnownColor(knownColors[random.Next(colors[pf].Min, colors[pf].Max)]));
                                /*Color.FromArgb((byte) this.random.Next(),
                                    (byte) this.random.Next(colors[pf].Min, colors[pf].Max),
                                    (byte) this.random.Next(colors[pf].Min, colors[pf].Max),
                                    (byte) this.random.Next(colors[pf].Min, colors[pf].Max)));

                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }*/
                    }
                }

            if (smoothingEnabled)
                SmoothEdges();
            fragmentsToUpdate.Clear();
            fragmentHated = null;
            fragmentLiked = null;
        }

        private void SmoothEdges()
        {
            var d = 5;
            //Apply your Gaussian blur method to the image

            //(for example, with AForge.NET, you might use the following code:)
            var filter = new GaussianBlur(15, 15);


            filter.ApplyInPlace(Painting, new Rectangle(0, height/2 - d, width, 2*d));

            filter.ApplyInPlace(Painting, new Rectangle(width/2 - d, 0, 2*d, height));

            //                filter.ApplyInPlace(Painting);
        }

        public void ReviewPainting(int x, int y, bool likedIt)
        {
            ReviewPainting(CoordsToPaintingFragment(x, y), likedIt);
        }

        protected abstract void ChangeNotLikedFragmentToLiked(PaintingFragment fragment);
        protected abstract void ChangHatedFragment();

        public void ReviewPainting(PaintingFragment fragment, bool likedIt)
        {
            if (likedIt)
            {
                fragmentLiked = fragment;

                //if (Mode == PaintingMode.GoogleTopicsImages)
                {
                    foreach (PaintingFragment value in Enum.GetValues(typeof (PaintingFragment)))
                    {
                        if (value != fragmentLiked.Value)
                        {
                            ChangeNotLikedFragmentToLiked(value);
                        }
                    }
                }
            }
            else
            {
                fragmentHated = fragment;
                ChangHatedFragment();
            }


            UpdatePainting();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}