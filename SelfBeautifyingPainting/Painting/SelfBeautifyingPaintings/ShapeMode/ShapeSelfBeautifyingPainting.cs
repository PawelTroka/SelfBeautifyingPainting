using System;
using System.Collections.Generic;
using System.Drawing;

namespace SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ShapeMode
{
    internal sealed class ShapeSelfBeautifyingPainting : SelfBeautifyingPainting
    {
        private readonly Dictionary<PaintingFragment, ShapeDrawer> shapeDrawers;

        public ShapeSelfBeautifyingPainting(int w, int h) : base(w, h)
        {
            shapeDrawers = new Dictionary<PaintingFragment, ShapeDrawer>();
            foreach (var value in Enum.GetValues(typeof (PaintingFragment)))
            {
                shapeDrawers[(PaintingFragment) value] = new ShapeDrawer();
            }

            UpdatePainting();
        }

        protected override void UpdatePainting()
        {
            var g = Graphics.FromImage(Painting);
            foreach (PaintingFragment pf in Enum.GetValues(typeof (PaintingFragment)))
            {
                if (DoesPaintingFragmentNeedRepaint(pf))
                {
                    clearFragment(pf);
                    var coordsRange = PaintingFragmentToRange(pf);
                    shapeDrawers[pf].DrawMany(g, coordsRange.Min.X, coordsRange.Max.X, coordsRange.Min.Y,
                        coordsRange.Max.Y);
                }
            }

            fragmentsToUpdate.Clear();
            fragmentHated = null;
            fragmentLiked = null;
            
        }

        protected override void ChangeNotLikedFragmentToLiked(PaintingFragment fragment)
        {
            if (fragmentLiked != null)
            {
                shapeDrawers[fragment] = new ShapeDrawer(shapeDrawers[fragmentLiked.Value]);
            }
        }

        protected override void ChangHatedFragment()
        {
            //    if (Mode == PaintingMode.GoogleTopicsImages)
            {
                if (fragmentHated != null)
                {
                    shapeDrawers[fragmentHated.Value] = new ShapeDrawer();
                }
            }
        }
    }
}