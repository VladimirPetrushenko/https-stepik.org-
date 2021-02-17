using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public class ShadeOfGradeFilter : PixelFilter
    {
        public ShadeOfGradeFilter() : base(new GrayscaleParameters())
        {
        }

        public override string ToString()
        {
            return "Конверт в черно-белое изображение";
        }

        public override Pixel ProcessPixel(Pixel original, IParameters parameters)
        {
            double graduentGray = (original.R * 0.199 + original.G * 0.567 + original.B * 0.14);
            return new Pixel(graduentGray, graduentGray, graduentGray);
        }
    }
}
