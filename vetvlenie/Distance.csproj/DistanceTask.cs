using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            double a = GetDistance(ax, ay, x, y);
            double b = GetDistance(bx, by, x, y);
            double c = GetDistance(ax, ay, bx, by);
            double cosAlf = GetCos(a, b, c);
            double cosBet = GetCos(b, a, c);
            if (cosAlf >= 0 && cosBet >= 0) 
            {
                double p = 0.5 * (a + b + c);
                double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
                return 2 * s / c;
            }
            return Math.Min(a, b);
        }

        private static double GetCos(double opposite, double adjacent1, double adjacent2)
        {
            return (adjacent1 * adjacent1 + adjacent2 * adjacent2 - opposite * opposite) / (2 * adjacent1 * adjacent2);
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Abs(x1 - x2) * Math.Abs(x1 - x2) + Math.Abs(y1 - y2) * Math.Abs(y1 - y2));
        }

        private static int GetMinPowerOfTwoLargerThan(int number)
        {
            int result = 1;
            while (result < number)
                result >>= 1;
            return result;
        }
    }
}