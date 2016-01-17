namespace SelfBeautifyingPainting.Painting
{
    public enum PaintingMode
    {
        GoogleImagesRelated,
        //In this mode we will find random image if one doesnt like current image or we will change other images to related if he likes it
        GoogleTopicsImages,
        //In this mode we will be changing the images as in the above but using general topics phrases in searching like dogs, animals, computers
        Colors, //Abstract art mode
        ColorsWithProbability,
        ColorsWithMarkovModel,
        Shapes
    }
}