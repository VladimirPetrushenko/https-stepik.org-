using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public class ShadeOfGradeFilter : PixelFilter<GrayscaleParameters>
    {
        public override string ToString()
        {
            return "Конверт в черно-белое изображение";
        }

        public override Pixel ProcessPixel(Pixel original, GrayscaleParameters parameters)
        {
            double graduentGray = (original.R * 0.199 + original.G * 0.567 + original.B * 0.14);
            return new Pixel(graduentGray, graduentGray, graduentGray);
        }
    }
}
