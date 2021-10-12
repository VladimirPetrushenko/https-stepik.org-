using System;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
			if (width == height && width == 1)
				return original;
			var result = new double[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++) {
					if (IsInnerPixel(x, y, width, height))
						result[x, y] = GetMediane(GetSubMass(original, x - 1, y - 1, 3, 3));
					if (!IsCornerPixel(x, y, width, height) && y == 0)
						result[x, y] = GetMediane(GetSubMass(original, x - 1, y, 3, height > 2 ? 2 : height));
					if (!IsCornerPixel(x, y, width, height) && y == height - 1)
						result[x, y] = GetMediane(GetSubMass(original, x - 1, y > 0 ? y - 1 : y, 3, height > 2 ? 2 : height));
					if (!IsCornerPixel(x, y, width, height) && x == 0)
						result[x, y] = GetMediane(GetSubMass(original, x, y - 1, width > 2 ? 2 : width, 3));
					if (!IsCornerPixel(x, y, width, height) && x == width - 1)
						result[x, y] = GetMediane(GetSubMass(original, x > 0 ? x - 1 : x, y - 1, width > 2 ? 2 : width, 3));
				}
			ChangeCornerPixel(original, width, height, result);
            return result;
        }

        private static void ChangeCornerPixel(double[,] original, int width, int height, double[,] result)
        {
            result[0, 0] = GetMediane(GetSubMass(original, 0, 0, width > 2 ? 2 : width, height > 2 ? 2 : height));
			result[width - 1, 0] = GetMediane(GetSubMass(original, 
				width >= 2 ? width - 2 : width - 1, 
				0, 
				width > 2 ? 2 : width, 
				height > 2 ? 2 : height));
            result[0, height - 1] = GetMediane(GetSubMass(original, 
				0, 
				height >= 2 ? height - 2 : height - 1, 
				width > 2 ? 2 : width, 
				height > 2 ? 2 : height));
			result[width - 1, height - 1] = GetMediane(GetSubMass(original,
				width >= 2 ? width - 2 : width - 1,
				height >= 2 ? height - 2 : height - 1,
				width > 2 ? 2 : width, height > 2 ? 2 : height));
		}

        public static bool IsInnerPixel( int x, int y, int width, int height)
        {
			return x + 1 < width && x - 1 >= 0 && y + 1 < height && y - 1 >= 0;
        }

		public static bool IsCornerPixel(int x, int y, int width, int height)
		{
			return (x == 0 && y == 0) || (x == width - 1 && y == 0) 
					|| (x == 0 && y == height - 1) || (x == width - 1 && y == height - 1);
		}

		public static double[,] GetSubMass(double[,] origin, int startX, int startY, int width, int height)
        {
			var result = new double[width, height];
			for (int x = startX; x < startX + width; x++)
				for (int y = startY; y < startY + height; y++)
					result[x - startX, y - startY] = origin[x, y];
			return result;
        }

		public static double GetMediane(double[,] pixel)
        {
			var width = pixel.GetLength(0);
			var height = pixel.GetLength(1);
			var result = new double[width * height];
			for (int i = 0; i < width; i++)
				for (int j = 0; j < height; j++)
					result[i * height + j ] = pixel[i, j];
			Array.Sort(result);
			return result.Length % 2 == 0 ? (result[result.Length / 2] + result[result.Length / 2 - 1]) / 2 : result[result.Length / 2];
        }
	}
}