using System;
using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var result = new double[original.GetLength(0), original.GetLength(1)];
            var threshold = GetThreshold(original, whitePixelsFraction, width, height);
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++) { 
                    if (original[x, y] >= threshold)
                        result[x, y] = 1;
                    else
                        result[x, y] = 0;
                }
            return result;
        }

        private static double GetThreshold(double[,] original, double whitePixelsFraction, int width, int height)
        {
            var sortMass = new double[width * height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    sortMass[x * height + y] = original[x, y];
            Array.Sort(sortMass);
            var N = width * height;
            if (N * whitePixelsFraction < 1)
                return sortMass.Max() + 1;
            var T = N - (int)(N * whitePixelsFraction);
            return sortMass[T];
        }
    }
}