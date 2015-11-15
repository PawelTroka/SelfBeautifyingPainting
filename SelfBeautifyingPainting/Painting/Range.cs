using System;

namespace SelfBeautifyingPainting.Painting
{
    struct Range
    {
        public Range(byte min, byte max)
        {
            Min = min;
            Max = max;
        }

        public byte Min;
        public byte Max;
    }
}