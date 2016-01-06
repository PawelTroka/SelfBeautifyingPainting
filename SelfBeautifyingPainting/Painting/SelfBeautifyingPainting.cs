using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging.Filters;
using SelfBeautifyingPainting.Google;
using SelfBeautifyingPainting.Helpers;

namespace SelfBeautifyingPainting.Painting
{
    public struct Coords
    {
        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X, Y;
    }

    class SelfBeautifyingPainting
    {
        private readonly GoogleTopicsImageFinder googleTopicsImageFinder;

        private Dictionary<PaintingFragment, Range> colors;

        private Dictionary<PaintingFragment, string> topics;


        private Dictionary<PaintingFragment, Bitmap> images;

        private List<PaintingFragment> fragmentsToUpdate;

        private PaintingFragment? fragmentLiked;
        private PaintingFragment? fragmentHated;
        public PaintingMode Mode { get; set; }

        public Bitmap Painting { get; private set; }

        private readonly Random random = new Random();


        private PaintingFragment CoordsToPaintingFragment(int x, int y)
        {
            if( x <= width/2 && y<=height/2)
                return PaintingFragment.LeftTop;

            if (x > width / 2 && y > height / 2)
                return PaintingFragment.RightBottom;

            if (x > width / 2 && y <= height / 2)
                return PaintingFragment.RightTop;

            if (x <= width / 2 && y > height / 2)
                return PaintingFragment.LeftBottom;

            return PaintingFragment.LeftTop;
        }

        private Coords PaintingFragmentToCoordsOffset(PaintingFragment pf)
        {
            switch (pf)
            {
                case PaintingFragment.LeftTop:
                    return new Coords(0,0);
                case PaintingFragment.RightTop:
                    return new Coords(width/2,0);
                case PaintingFragment.LeftBottom:
                    return new Coords(0,height/2);
                case PaintingFragment.RightBottom:
                    return new Coords(width/2,height/2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(pf), pf, null);
            }
        }

        public SelfBeautifyingPainting(int w, int h)
        {
            height = h;
            width = w;
            googleTopicsImageFinder = new GoogleTopicsImageFinder(w,h);
            colors = new Dictionary<PaintingFragment, Range>();
            topics = new Dictionary<PaintingFragment, string>();

            images = new Dictionary<PaintingFragment, Bitmap>();
            fragmentsToUpdate = new List<PaintingFragment>();


            foreach (var value in Enum.GetValues(typeof (PaintingFragment)))
            {
                fragmentsToUpdate.Add((PaintingFragment) value);
                var randomByte1 = (byte) random.Next(byte.MinValue, byte.MaxValue);
                var randomByte2 = (byte) random.Next(randomByte1, byte.MaxValue);

                colors[(PaintingFragment) value] = new Range(randomByte1, randomByte2);
                topics[(PaintingFragment) value] = EnglishWordsDictionary.GetRandomWord();
                images[(PaintingFragment) value] = googleTopicsImageFinder.GetPicture(EnglishWordsDictionary.GetRandomWord());
            }


            Painting = new Bitmap(width, height);
            UpdatePainting();
        }

        /* public SelfBeautifyingPainting()
         {
             Painting = this.UpdatePainting();
         }*/

        public event EventHandler ImageChanged;

        private void UpdatePainting()
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    var pf = CoordsToPaintingFragment(x, y);

                    if ((!fragmentLiked.HasValue && !fragmentHated.HasValue) ||
                        (fragmentLiked.HasValue && pf != fragmentLiked.Value) ||
                        (fragmentHated.HasValue && pf == fragmentHated.Value))
                    {
                        var coordsOffset = PaintingFragmentToCoordsOffset(pf);
                        switch (Mode)
                        {
                            case PaintingMode.GoogleImagesRelated:
                            case PaintingMode.GoogleTopicsImages:

                                //if(x - coordsOffset.X < images[pf].Width &&  y - coordsOffset.Y< images[pf].Height)
                                Painting.SetPixel(x, y, images[pf].GetPixel(x - coordsOffset.X, y - coordsOffset.Y));
                                break;
                            case PaintingMode.Colors:

                                Painting.SetPixel(x, y,
                                    Color.FromArgb((byte) this.random.Next(),
                                        (byte) this.random.Next(colors[pf].Min, colors[pf].Max),
                                        (byte) this.random.Next(colors[pf].Min, colors[pf].Max),
                                        (byte) this.random.Next(colors[pf].Min, colors[pf].Max)));

                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
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
            GaussianBlur filter = new GaussianBlur(15, 15);


            filter.ApplyInPlace(Painting,new Rectangle(0,height/2-d,width,2*d));

            filter.ApplyInPlace(Painting, new Rectangle(width/2-d, 0, 2*d, height));

            //                filter.ApplyInPlace(Painting);

        }

        public void ReviewPainting(int x, int y, bool likedIt)
        {
            ReviewPainting(CoordsToPaintingFragment(x, y), likedIt);
        }

        public void ReviewPainting(PaintingFragment fragment, bool likedIt)
        {
            if (likedIt)
            {
                fragmentLiked = fragment;

                if (Mode == PaintingMode.GoogleTopicsImages)
                {
                    foreach (PaintingFragment value in Enum.GetValues(typeof(PaintingFragment)))
                    {
                        if (value != fragmentLiked.Value)
                        {
                            topics[value] = topics[fragmentLiked.Value];
                            images[value] = googleTopicsImageFinder.GetPicture(topics[fragmentLiked.Value]);
                        }
                    }
                }
            }
            else
            {
                fragmentHated = fragment;

                if (Mode == PaintingMode.GoogleTopicsImages)
                {
                    topics[fragmentHated.Value] = EnglishWordsDictionary.GetRandomWord();
                    images[fragmentHated.Value] = googleTopicsImageFinder.GetPicture(topics[fragmentHated.Value]);
                }
                else if (Mode == PaintingMode.GoogleImagesRelated)
                {
                    images[fragmentHated.Value] = googleTopicsImageFinder.GetPicture(EnglishWordsDictionary.GetRandomWord());
                }
            }



            UpdatePainting();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        /* public int Height
        {
            get { return height; }
            set
            {
                height = value;
                UpdatePainting();
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                UpdatePainting();
            }
        }*/

        private int height = 1080;
        private int width = 1920;
    }
}
