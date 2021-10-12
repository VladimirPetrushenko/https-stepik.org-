using System;
using System.Collections.Generic;

namespace Permutations
{
    class Program
    {
		public static void Main()
		{
			TestOnSize(1);
			TestOnSize(2);
			TestOnSize(0);
			TestOnSize(3);
			TestOnSize(4);
		}

		static void TestOnSize(int size)
		{
			var result = new List<int[]>();
			MakePermutations(new int[size], 0, result);
			foreach (var permutation in result)
				WritePermutation(permutation);
		}

		static void WritePermutation(int[] permutation)
		{
			var strings = new List<string>();
			foreach (var i in permutation)
				strings.Add(i.ToString());
			Console.WriteLine(string.Join(" ", strings.ToArray()));
		}

		static void MakePermutations(int[] permutation, int position, List<int[]> result)
		{
			if (position == permutation.Length)
			{
				result.Add(permutation);
			}
			else
			{
				for (int i = 0; i < permutation.Length; i++)
				{
					var index = Array.IndexOf(permutation, i, 0, position);
					if (index == -1)
					{
						permutation[position] = i;
						var temp = new int[permutation.Length];
						permutation.CopyTo(temp, 0);
						MakePermutations(temp, position + 1, result);
					}
				}
			}
		}
	}
}
