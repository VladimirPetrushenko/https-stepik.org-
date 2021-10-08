using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IsCorrectMove
{
    class Program
    {
        enum Suits
        {
            Wands,
            Coins,
            Cups,
            Swords
        }

        public enum Mark
        {
            Empty,
            Cross,
            Circle
        }

        public enum GameResult
        {
            CrossWin,
            CircleWin,
            Draw
        }

        private static Dictionary<string, List<string>> OptimizeContacts(List<string> contacts)
        {
            var dictionary = new Dictionary<string, List<string>>();
            foreach (var contact in contacts)
            {
                string key = contact.Split(':')[0].Substring(0, contact.Split(':')[0].Length >= 2 ? 2 : contact.Split(':')[0].Length);
                if (dictionary.ContainsKey(key))
                    dictionary[key].Add(contact.Split(':')[1]);
                else
                    dictionary[key] = new List<string>() { contact.Split(':')[1] };
            }


            return dictionary;
        }
        public static void PrintNumbers(int[] a)
        {
            Console.Write(string.Join(",", a));
            Console.WriteLine();
        }
        public static int[] GetBenfordStatistics(string text)
        {
            var statistics = new int[10];
            foreach (var word in text.Split(' '))
                if (char.IsDigit(word[0]))
                    statistics[int.Parse(word[0].ToString())]++;
            return statistics;
        }
        public static string ReplaceIncorrectSeparators(string text)
        {
            return text.Replace(":", "").Replace("-", "").Replace(",", "").Replace(";", "").Replace(" ", "\t");
            //TODO ваше решение
        }
        private static string ApplyCommands(string[] commands)
        {
            StringBuilder text = new StringBuilder();
            foreach(var command in commands) { 
                if (command.Contains("push"))
                {
                    text.Append(command.Substring(4));
                }
                else
                {
                    text.Remove(text.Length - int.Parse(command.Substring(4)) - 1, int.Parse(command.Substring(4)));
                }
            }
            return text.ToString();
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Encoding.UTF8.GetBytes("БЩФzw!").Length);
        }

        private static void Run(string description)
        {
            Console.WriteLine(description.Replace(" ", Environment.NewLine));
            Console.WriteLine(GetGameResult(CreateFromString(description)));
            Console.WriteLine();
        }

        private static Mark[,] CreateFromString(string str)
        {
            var field = str.Split(' ');
            var ans = new Mark[3, 3];
            for (int x = 0; x < field.Length; x++)
                for (var y = 0; y < field.Length; y++)
                    ans[x, y] = field[x][y] == 'X' ? Mark.Cross : (field[x][y] == 'O' ? Mark.Circle : Mark.Empty);
            return ans;
        }

        public static void TestMove(string from, string to)
        {
            Console.WriteLine("{0}-{1} {2}", from, to, IsCorrectMove(from, to));
        }

        public static bool IsCorrectMove(string from, string to)
        {
            var dx = Math.Abs(to[0] - from[0]); //смещение фигуры по горизонтали
            var dy = Math.Abs(to[1] - from[1]); //смещение фигуры по вертикали
            return (dx == dy || dy == 0 || dx == 0) && !(dy==0 && dx==0);
        }

        static bool ShouldFire2(bool enemyInFront, string enemyName, int robotHealth)
        {
            return enemyInFront && (enemyName == "boss" && robotHealth >= 50 || enemyName != "boss");
        }

        private static int GetMinPowerOfTwoLargerThan(int number)
        {
            int result = 1;
            while (result <= number)
                result <<= 1;
            return result;
        }

        public static string RemoveStartSpaces(string text)
        {
            while (true)
            {
                if (text.Length>0 && char.IsWhiteSpace(text[0])) text = text.Substring(1);
                else return text;
            }
        }
        private static void WriteTextWithBorder(string text)
        {
            WriteBorder(text);
            Console.WriteLine("| " + text + " |");
            WriteBorder(text);
        }

        private static void WriteBorder(string text)
        {
            Console.Write('+');
            for (int i = 0; i < text.Length + 2; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine('+');
        }
        private static void WriteBoard(int size)
        {
            char sharp = '#';
            char dot = '.';
            for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(j % 2 == 0 ? dot : sharp);
                }
                Console.WriteLine();
                SwapChar(ref sharp, ref dot);
            }
            Console.WriteLine();
        }

        private static void SwapChar(ref char sharp, ref char dot)
        {
            char temp = dot;
            dot = sharp;
            sharp = temp;
        }
        private static string GetSuit(Suits suit)
        {
            return new[] { "жезлов", "монет", "кубков", "мечей" }[(int)suit];
        }

        public static bool CheckFirstElement(int[] array)
        {
            
            return array != null && array.Length != 0 && array[0] == 0;
        }

        public static GameResult GetGameResult(Mark[,] field)
        {
            if (CheckWin(field, Mark.Cross))
                return GameResult.CrossWin;
            if (CheckWin(field, Mark.Circle))
                return GameResult.CircleWin;
            return GameResult.Draw;
        }
        public static bool CheckWin(Mark[,] field, Mark mark)
        {
            return CheckColumn(field, mark) || CheckRows(field, mark) || CheckDiagonal(field, mark);
        }
        public static bool CheckRows(Mark[,] field, Mark mark)
        {
            int winMark = 0;
            for (int i = 0; i < 3 && winMark < 3; i++) 
            { 
                winMark = 0;
                for (int j = 0; j < 3; j++) {
                    if (field[i, j] != mark)
                        break;
                    else
                        winMark++;
                }
            }
            return winMark >= 3;
        }

        public static bool CheckColumn(Mark[,] field, Mark mark)
        {
            int winMark = 0;
            for (int i = 0; i < 3 && winMark < 3; i++)
            {
                winMark = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (field[j, i] != mark)
                        break;
                    else
                        winMark++;
                }
            }
            return winMark >= 3;
        }

        public static bool CheckDiagonal(Mark[,] field, Mark mark)
        {
            int winMark = 0;
            for (int i = 0; i < 3; i++)
            {
                if (field[i, i] != mark)
                    break;
                else
                    winMark++;
            }
            if (winMark >= 3)
                return true;
            winMark = 0;
            for (int i = 0; i < 3; i++)
            {
                if (field[2 - i, i] != mark)
                    break;
                else
                    winMark++;
            }
            return winMark >= 3;
        }
    }
    
}
