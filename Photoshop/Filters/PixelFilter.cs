using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    abstract public class PixelFilter<Tparams> : ParametrizedFilter <Tparams>
                                    where Tparams : IParameters, new()
    {
        public override Photo Process(Photo original, Tparams parameters)
        {
            var result = new Photo(original.width, original.height);

            for (int x = 0; x < result.width; x++)
                for (int y = 0; y < result.height; y++)
                {
                    result[x, y] = ProcessPixel(original[x, y], parameters);
                }
            return result;
        }
        abstract public Pixel ProcessPixel(Pixel original, Tparams parameters);
    }
}
