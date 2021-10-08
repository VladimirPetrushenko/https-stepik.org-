using System;
using System.Drawing;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			double x = 1, y = 0; 
			Random rand = new Random(seed);
			for(int i = 0; i < iterationsCount; i++)
            {
                if (rand.Next() % 2 == 0)
                {
                    double x1 = (x * Math.Cos(Math.PI / 4) - y * Math.Sin(Math.PI / 4)) / Math.Sqrt(2);
                    double y1 = (x * Math.Sin(Math.PI / 4) + y * Math.Cos(Math.PI / 4)) / Math.Sqrt(2);
					x = x1;
					y = y1;
                }
                else
				{
                    double x1 = (x * Math.Cos(3 * Math.PI / 4) - y * Math.Sin(3 * Math.PI / 4)) / Math.Sqrt(2) + 1;
                    double y1 = (x * Math.Sin(3 * Math.PI / 4) + y * Math.Cos(3 * Math.PI / 4)) / Math.Sqrt(2);
					x = x1;
					y = y1;
                }

				pixels.SetPixel(x, y);
            }
		}
    }
}