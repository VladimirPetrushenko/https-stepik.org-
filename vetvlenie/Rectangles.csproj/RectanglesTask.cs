using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
            // так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top
            bool r2inr1x = r1.Left <= r2.Left && r1.Right >= r2.Left;
			bool r1inr2x = r2.Left <= r1.Left && r2.Right >= r1.Left;

			bool r2inr1y = r1.Top <= r2.Top && r1.Bottom >= r2.Top;
			bool r1inr2y = r2.Top <= r1.Top && r2.Bottom >= r1.Top;

			return r2inr1x && (r2inr1y || r1inr2y) || r1inr2x && (r2inr1y || r1inr2y);
		}

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (r1.Width == 0 || r1.Height == 0 || r2.Height == 0 || r2.Width == 0)
                return 0;
            if(AreIntersected(r1, r2))
            {
                (int x1, int y1) = GetLeftmostPoint(r1, r2);
                (int x2, int y2) = GetRightmostPoint(r1, r2);
                return (y2 - y1) * (x2 - x1);
            }

            return 0;
        }

        private static (int,int) GetRightmostPoint(Rectangle r1, Rectangle r2)
        {
            int x = GetMin(r1.Right, r2.Right);
            int y = GetMin(r1.Bottom, r2.Bottom);
            return (x, y);
        }

        private static (int, int) GetLeftmostPoint(Rectangle r1, Rectangle r2)
        {
            int x = GetMax(r1.Left, r2.Left);
            int y = GetMax(r1.Top, r2.Top);
            return (x, y);
        }

        private static int GetMax(int r1, int r2)
        {
            return Math.Max(r2, r1);
        }

        private static int GetMin(int r1, int r2)
        {
            return Math.Min(r2, r1);
        }
        // Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
        // Иначе вернуть -1
        // Если прямоугольники совпадают, можно вернуть номер любого из них.
        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (IsContainedRectangle(r1, r2))
                return 1;
            if (IsContainedRectangle(r2, r1))
                return 0;
            return -1;
        }

        private static bool IsContainedRectangle(Rectangle r1, Rectangle r2)
        {
            return r1.Left <= r2.Left && r1.Right >= r2.Right && r1.Top <= r2.Top && r1.Bottom >= r2.Bottom;
        }
    }
}