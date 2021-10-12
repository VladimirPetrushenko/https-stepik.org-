using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var distances = CreateDistancesArray(checkpoints);
			var bestOrder = MakeTrivialPermutation(checkpoints.Length);
            var result = new List<int[]>();
            MakePermutations(new int[checkpoints.Length], 1, result);
            var min = double.MaxValue;
            var index = 0;
            for (int i = 0; i < result.Count; i++)
            {
                var temp = Evaluate(result[i], distances);
                if (min > temp)
                {
                    min = temp;
                    index = i;
                }
                    
            }
            return result[index];
		}

		private static int[] MakeTrivialPermutation(int size)
		{
			var bestOrder = new int[size];
			for (int i = 0; i < bestOrder.Length; i++)
				bestOrder[i] = i;
			return bestOrder;
		}
		
		private static double[,] CreateDistancesArray(Point[] points)
        {
			var result = new double[points.Length, points.Length];
            for (int i = 0; i < points.Length; i++) { 
				var pointX = points[i].X;
				var pointY = points[i].Y;
				result[i, i] = 0;
                for (int j = i; j < points.Length; j++)
                {
                    var dx = points[j].X - pointX;
                    var dy = points[j].Y - pointY;
                    result[i, j] = Math.Sqrt(dx * dx + dy * dy);
                    result[j, i] = result[i, j];
                }
			}
			return result;
        }

        static double Evaluate(int[] permutation, double[,] prices)
        {
            double price = 0;
            for (int i = 0; i < permutation.Length - 1; i++)
                price += prices[permutation[i], permutation[(i + 1) % permutation.Length]];
            return price;
        }

        static void MakePermutations(int[] permutation, int position, List<int[]> result)
        {
            if (position == permutation.Length)
            {
                result.Add(permutation);
                return;
            }

            for (int i = 1; i < permutation.Length; i++)
            {
                var index = Array.IndexOf(permutation, i, 1, position);
                if (index != -1)
                    continue;
                permutation[position] = i;
                var temp = new int[permutation.Length];
                permutation.CopyTo(temp, 0);
                MakePermutations(temp, position + 1, result);
            }
        }
    }
}