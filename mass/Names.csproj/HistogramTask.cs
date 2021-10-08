using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var days = new string[31];
            for (int i = 0; i < days.Length; i++)
            {
                days[i] = (i + 1).ToString();
            }
            var count = new double[31];
            foreach (var person in names)
            {
                if (person.Name == name)
                    if (person.BirthDate.Day != 1)
                        count[person.BirthDate.Day - 1]++;
            }
            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name), 
                days, 
                count);
        }
    }
}