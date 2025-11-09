using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GUI
{
    public class algorithm
    {
        private char[][] grid;
        private Random random;
        private List<string> words;

        public algorithm()
        {
            random = new Random();
            grid = new char[0][];
            words = new List<string>();
        }
        // Основной метод генерации массива
        public char[][] Generate(List<string> inputWords)  // Метод, который возвращает рваный массив 
        {
            if (inputWords == null || inputWords.Count == 0)
                throw new ArgumentException("Список слов не может быть пустым");

            var words = inputWords.Select(w => w.Trim().ToUpper()) // Тут приводим слова к верхнему регистру 
                .Where(w => !string.IsNullOrEmpty(w))             // и убираем пробелы
                .ToList();

            if (words.Count == 0)
                throw new ArgumentException("Нет валидных слов для генерации");

            words = words.OrderByDescending(w => w.Length).ToList(); // Сортировка слов по длине

            BuildCrossword(); // Построение кроссворда
            return grid;
        }
        private void BuildCrossword()
        {
            PlaceFirstWord(words[0]); // Размещ первое слово

            for (int i = 1; i < words.Count; i++) // Размещ остальные слова
            {
                if (!TryPlaceWord(words[i]))
                {
                    Console.WriteLine("Не удалось разместить слово: {words[i]}");
                }
            }
        }
        private void PlaceFirstWord(string word) // Размещ первого слова
        {

            grid = new char[1][]; // Созд массив из одной строки с первым словом
            grid[0] = word.ToCharArray();

            Console.WriteLine("Размещено первое слово: {word}");
        }
        private bool TryPlaceWord(string word) // Размещ слово в существующей структуре
        {
            for (int attempt = 0; attempt < 50; attempt++) // Разные попытки
            {
                bool horizontal = random.Next(2) == 0; // Тип рандомно ставим либо горизонтально, либо вертикально 

                if (TryFindAndPlace(word, horizontal))
                    return true;

                if (TryFindAndPlace(word, !horizontal))
                    return true;
            }
            return false;
        }
        private bool TryFindAndPlace(string word, bool horizontal)
        {
            var intersections = FindAllIntersections(word); // Ищем все возможные пересечения

            foreach (var intersection in intersections)
            {
                if (horizontal && CanPlaceHorizontal(word, intersection))
                {
                    return true;
                }
                else if (!horizontal && CanPlaceVertical(word, intersection))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

