namespace MyPhotoshop
{
    public interface IPixelFilter
    {
        Photo Process(Photo original, IParameters parameters);
    }
}