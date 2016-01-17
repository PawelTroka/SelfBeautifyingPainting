using System;
using System.Collections.Generic;
using System.Drawing;
using SelfBeautifyingPainting.Google;
using SelfBeautifyingPainting.Helpers;

namespace SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings
{
    internal class GoogleTopicsSelfBeautifyingPainting : SelfBeautifyingPainting
    {
        private readonly GoogleTopicsImageFinder googleTopicsImageFinder;

        private readonly Dictionary<PaintingFragment, Bitmap> images;

        private readonly Dictionary<PaintingFragment, string> topics;


        public GoogleTopicsSelfBeautifyingPainting(int w, int h) : base(w, h)
        {
            googleTopicsImageFinder = new GoogleTopicsImageFinder(w, h);

            topics = new Dictionary<PaintingFragment, string>();

            images = new Dictionary<PaintingFragment, Bitmap>();

            foreach (var value in Enum.GetValues(typeof (PaintingFragment)))
            {
                topics[(PaintingFragment) value] = EnglishWordsDictionary.GetRandomWord();
                images[(PaintingFragment) value] = googleTopicsImageFinder.GetPicture(topics[(PaintingFragment) value]);
            }

            updateFunction = (fragment, x, y) => images[fragment].GetPixel(x, y);
            UpdatePainting();
        }


        //   public override event EventHandler ImageChanged;

        protected override void ChangeNotLikedFragmentToLiked(PaintingFragment fragment)
        {
            if (fragmentLiked != null)
            {
                topics[fragment] = topics[fragmentLiked.Value];
                images[fragment] = googleTopicsImageFinder.GetPicture(topics[fragmentLiked.Value]);
            }
        }

        protected override void ChangHatedFragment()
        {
            if (fragmentHated != null)
            {
                topics[fragmentHated.Value] = EnglishWordsDictionary.GetRandomWord();
                images[fragmentHated.Value] = googleTopicsImageFinder.GetPicture(topics[fragmentHated.Value]);
            }
        }
    }
}