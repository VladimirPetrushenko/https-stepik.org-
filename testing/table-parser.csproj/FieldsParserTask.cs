using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("''", new[] { "" })]
        [TestCase("text", new[] { "text" })]
        [TestCase("hello    world", new[] { "hello", "world" })]
        [TestCase(" a ", new[] { "a" })]
        [TestCase("\"start with double' 234\"", new[] { "start with double' 234" })]
        [TestCase("\'\"aa\"\'", new[] { "\"aa\"" })]
        [TestCase("\'\"aa\"", new[] { "\"aa\"" })]
        [TestCase("\"\'aa\'", new[] { "\'aa\'" })]
        [TestCase("b \"a'\"", new[] { "b", "a'" })]
        [TestCase("\"a'\"b", new[] { "a'", "b" })]
        [TestCase(@"'a\' b'", new[] { "a' b" })]
        [TestCase(@"'a\\'", new[] { @"a\" })]
        [TestCase("   ", new string[] { })]
        [TestCase("\' ", new[] { " " })]
        [TestCase("\"\\\"\"", new[] { "\"" })]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        // При решении этой задаче постарайтесь избежать создания методов, длиннее 10 строк.
        // Подумайте как можно использовать ReadQuotedField и Token в этой задаче.
        //"a \"bcd ef\" 'x y'",["a", "bcd ef", "x y"]
        public static List<Token> ParseLine(string line)
        {
            var result = new List<Token>();
            for (int startIndex = 0; startIndex<line.Length; )
            {
                if (!char.IsWhiteSpace(line[startIndex]))
                {
                    Read(line, result, startIndex);
                    startIndex = result.Last().Position + result.Last().Length;
                }
                else
                    startIndex++;
            }
            return result; // сокращенный синтаксис для инициализации коллекции.
        }

        private static void Read(string line, List<Token> result, int startIndex)
        {
            if (IsQuated(line[startIndex]))
                result.Add(ReadQuotedField(line, startIndex));
            else
                result.Add(ReadField(line, startIndex));
        }

        private static Token ReadField(string line, int startIndex)
        {
            int end = startIndex;
            for (int index = startIndex + 1; index < line.Length; index++)
            {
                if (IsQuated(line[index]) || line[index] == ' ')
                    break;
                end = index;
            }
            var result = line.Substring(startIndex, end - startIndex + 1);
            return new Token(result, startIndex, end - startIndex + 1);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }

        public static bool IsQuated(char letter)
        {
            return letter == '\"' || letter == '\'';
        }
    }
}