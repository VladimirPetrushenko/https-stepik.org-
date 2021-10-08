using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var days = new string[30];
            for (int i = 0; i < days.Length; i++)
                days[i] = (i + 2).ToString();
            var month = new string[12];
            for (int i = 0; i < month.Length; i++)
                month[i] = (i + 1).ToString();

            var data = new double[30, 12];

            foreach(var name in names)
                if (name.BirthDate.Day != 1)
                    data[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;

            return new HeatmapData(
                "Пример карты интенсивностей",
                data, 
                days, 
                month);
        }
        
    }
}