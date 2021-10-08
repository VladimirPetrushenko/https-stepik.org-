using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            for(int i = 0; i < wordsCount; i++)
            {
                var words = phraseBeginning.Split(' ');
                if(words.Length >= 2)
                {
                    if (nextWords.ContainsKey(words[words.Length - 2] + " " + words[words.Length - 1]))
                        phraseBeginning += " " + nextWords[words[words.Length - 2] + " " + words[words.Length - 1]];
                    else if (nextWords.ContainsKey(words[words.Length - 1]))
                        phraseBeginning += " " + nextWords[words[words.Length - 1]];
                    else
                        break;
                }
                else if(words.Length == 1)
                {
                    if (nextWords.ContainsKey(words[words.Length - 1]))
                        phraseBeginning += " " + nextWords[words[words.Length - 1]];
                    else
                        break;
                }
            }
            return phraseBeginning;
        }
    }
}