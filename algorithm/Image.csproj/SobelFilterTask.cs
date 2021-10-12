using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var start = sx.GetLength(1) / 2;
            for (int x = start; x < width - start; x++)
                for (int y = start; y < height - start; y++)
                    result[x, y] = GetConvolution(g, x - start, y - start, sx);
            return result;
        }

        public static double GetConvolution(double[,] image, int x, int y, double[,] s)
        {
            var magX = 0.0;
            var magY = 0.0;
            var rows = s.GetLength(0);
            var cellings = s.GetLength(1);
            for (int row = 0; row < rows; row++)
                for (int cell = 0; cell < cellings; cell++)
                {
                    magX += image[row + x, cell + y] * s[row, cell];
                    magY += image[row + x, cell + y] * s[cell, row];
                }
            return Math.Sqrt(magX * magX + magY * magY);
        }
    }
}