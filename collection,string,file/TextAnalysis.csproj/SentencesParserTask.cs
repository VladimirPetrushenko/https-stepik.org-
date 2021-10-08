using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            string[] sentences = text.Split(new char[] { '.', '!', '?', ';', ':', '(', ')' });
            Regex regex = new Regex(@"[^\w']|[\d]");
            foreach(var sentence in sentences)
            {
                List<string> sentenceOfWords = new List<string>();
                var words = regex.Replace(sentence, " ").Split(new char[] { ' ' });
                foreach (var word in words)
                    if(word.Length>0)
                        sentenceOfWords.Add(word.ToLower());
                if (sentenceOfWords.Count > 0)
                    sentencesList.Add(sentenceOfWords);
            }
            return sentencesList;
        }
    }
}