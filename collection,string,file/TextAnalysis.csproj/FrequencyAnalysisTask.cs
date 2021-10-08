using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = CreateBigrams(text);
            var result2 = CreateThreegramms(text);
            result = result.Concat(result2).ToDictionary(x => x.Key, x => x.Value);
            return result;
        }

        public static Dictionary<string,string> CreateBigrams(List<List<string>> text)
        {
            var bigrams = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
                for (int i = 0; i < sentence.Count - 1; i++)
                    AddEntry(bigrams, sentence[i], sentence[i + 1]);
            return CreateDictionary(bigrams);
        }

        public static Dictionary<string, string> CreateThreegramms(List<List<string>> text)
        {
            var threegrams = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
                for (int i = 0; i < sentence.Count - 2; i++)
                    AddEntry(threegrams, sentence[i] + " " + sentence[i + 1], sentence[i + 2]);
            return CreateDictionary(threegrams);
        }

        private static void AddEntry(Dictionary<string, Dictionary<string, int>> dict, string key, string val)
        {
            if (dict.ContainsKey(key))
            {
                if (dict[key].ContainsKey(val))
                    dict[key][val]++;
                else
                    dict[key][val] = 0;
            }
            else
            {
                dict[key] = new Dictionary<string, int>();
                dict[key][val] = 0;
            }
        }

        private static Dictionary<string, string> CreateDictionary(Dictionary<string, Dictionary<string, int>> dict)
        {
            var result = new Dictionary<string, string>();
            foreach (var entry in dict)
            {
                var maxCount = entry.Value.Max(x => x.Value);
                var values = entry.Value.Where(x => x.Value == maxCount).Select(x => x.Key);
                result[entry.Key] = FindMinOrdinalString(values);
            }

            return result;
        }

        private static string FindMinOrdinalString(IEnumerable<string> keys)
        {
            var result = keys.FirstOrDefault();
            foreach (var key in keys)
            {
                if (string.CompareOrdinal(result, key) > 0)
                    result = key;
            }
            return result;
        }
    }
}