namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			// Напишите функцию склонения слова "рублей" в зависимости от предшествующего числительного count.
			count = count % 100;
			if (count == 1 || (count % 10 == 1 && count > 20))
				return "рубль";
			else if (count % 10 > 1 && count % 10 < 5 && (count > 20 || count < 10)) 
				return "рубля";
			else return "рублей";
		}
	}
}