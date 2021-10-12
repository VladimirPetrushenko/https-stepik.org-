using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("\"abc\"", 0, "abc", 5)]
        [TestCase("b \"a'\"", 2, "a'", 4)]
        [TestCase(@"'a\' b'", 0, "a' b", 7)]
        [TestCase(@"'a\\'", 0, @"a\", 5)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            int currentPosition = startIndex + 1;
            int end;
            while (true)
            {
                end = line.IndexOf(line[startIndex], currentPosition);
                if (end > 0)
                {
                    if (CheckEscapeSlash(line, end))
                        break;
                    else
                        currentPosition = end + 1;
                }
                else
                    break;
            }
            var result = end > 0 ? line.Substring(startIndex + 1, end - startIndex - 1) 
                                : line.Substring(startIndex + 1);
            result = result.Replace("\\\"", "\"").Replace("\\\'", "\'").Replace("\\\\", "\\");
            return new Token(result, startIndex, end > startIndex ? end - startIndex + 1 : line.Length - startIndex);
        }

        private static bool CheckEscapeSlash(string line, int end)
        {
            return line[end - 1] != '\\' || (CheckSlashSymbol(line, end));
        }

        private static bool CheckSlashSymbol(string line, int end)
        {
            return line[end - 1] == '\\' && line[end - 2] == '\\';
        }
    }
}
