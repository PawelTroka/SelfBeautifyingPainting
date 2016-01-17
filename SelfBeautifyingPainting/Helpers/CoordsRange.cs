namespace SelfBeautifyingPainting.Helpers
{
    public struct CoordsRange
    {
        public CoordsRange(Coords min, Coords max)
        {
            Max = max;
            Min = min;
        }

        public Coords Min;
        public Coords Max;
    }
}