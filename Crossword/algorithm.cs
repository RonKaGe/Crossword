
using GUI;
namespace CrosswordGen
{
    public class PlacedWord
    {
        public string Word;
        public int Row;
        public int Col;
        public bool IsHorizontal;
    }
    public class algorithm
    {
        private char[][] grid;
        private Random random;
        private List<string> words;
        private List<PlacedWord> placedWords = new List<PlacedWord>();

        public List<PlacedWord> FindWordsByCell(int r, int c)
        {
            var result = new List<PlacedWord>();

            foreach (var w in placedWords)
            {
                if (w.IsHorizontal)
                {
                    if (r == w.Row && c >= w.Col && c < w.Col + w.Word.Length)
                        result.Add(w);
                }
                else
                {
                    if (c == w.Col && r >= w.Row && r < w.Row + w.Word.Length)
                        result.Add(w);
                }
            }

            return result;
        }

        public void AskUserForCoordinates()
        {
            while (true)
            {
                Console.Write("\nВведите номер строки (или -1 для выхода): ");
                if (!int.TryParse(Console.ReadLine(), out int r) || r < 0)
                    break;

                Console.Write("Введите номер столбца: ");
                if (!int.TryParse(Console.ReadLine(), out int c))
                    break;

                var words = FindWordsByCell(r, c);

                if (words.Count == 0)
                {
                    Console.WriteLine("Эта клетка не принадлежит ни одному слову.");
                }
                else
                {
                    Console.WriteLine("Слова, которым принадлежит эта буква:");
                    foreach (var w in words)
                    {
                        Console.WriteLine($" - {w.Word} ({(w.IsHorizontal ? "горизонтально" : "вертикально")})");
                    }
                }
            }
        }


        public algorithm()
        {
            random = new Random();
            visualBox menu = new visualBox();
            words = menu.ListOfWords();
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
                    Console.WriteLine($"Не удалось разместить слово: {words[i]}");
                }
            }
        }
        private void PlaceFirstWord(string word) // Размещ первого слова
        {

            grid = new char[1][]; // Созд массив из одной строки с первым словом
            grid[0] = word.ToCharArray();

            Console.WriteLine($"Размещено первое слово: {word}");
        }
        private bool TryPlaceWord(string word) // Размещ слово в существующей структуре
        {
            for (int attempt = 0; attempt < 50; attempt++) // Разные попытки
            {
                bool placeVertical = (attempt % 2 == 0);

                if (TryFindAndPlace(word, placeVertical))
                    return true;

            }
            return false;
        }
        private bool TryFindAndPlace(string word, bool horizontal)

        {
            var intersections = FindAllIntersections(word); // Ищем все возможные пересечения
            var shuffledIntersections = intersections.OrderBy(x => random.Next()).ToList();

            foreach (var intersection in shuffledIntersections)
            {
                if (horizontal && CanPlaceHorizontal(word, intersection))
                {
                    PlaceHorizontal(word, intersection);
                    return true;
                }
                else if (!horizontal && CanPlaceVertical(word, intersection))
                {
                    PlaceVertical(word, intersection);
                    return true;
                }
            }
            return false;

   
        }

        private List<(int row, int col, char letter, int wordIndex)> FindAllIntersections(string word)
        {
            var intersections = new List<(int row, int col, char letter, int wordIndex)>(); // это нужно, чтобы вставить новое слово и определить,
                                                                                            // куда оно пойдёт (координаты строки и столбца, какие буквы будут стоять, их координата в самом слове

            for (int row = 0; row < grid.Length; row++)
            {
                if (grid[row] == null) continue;

                for (int col = 0; col < grid[row].Length; col++)
                {
                    char currentChar = grid[row][col]; // Игнор пустых клеток
                    if (currentChar != '\0')
                    {
                        // ищем совпадения букв
                        for (int wordIndex = 0; wordIndex < word.Length; wordIndex++)
                        {
                            if (word[wordIndex] == currentChar)
                            {
                                intersections.Add((row, col, currentChar, wordIndex));
                            }
                        }
                    }
                }
            }
            return intersections;
        }

        // проверка возможности размещения горизонтального слова
        private bool CanPlaceHorizontal(string word, (int row, int col, char letter, int wordIndex) intersections)
        {
            int startCol = intersections.col - intersections.wordIndex; // вычисл начальный столбец для слова
            int endCol = startCol + word.Length - 1; // вычисл конечный столбец для слова
            if (startCol < 0)
                return false;

            int currentRow = intersections.row;
            int currentRowLenght = grid[currentRow].Length;

            // проверяем, нет ли конфликтов с существующими буквами
            for (int i = 0; i < word.Length; i++)
            {
                int checkCol = startCol + i;

                if (checkCol < currentRowLenght && checkCol >= 0)
                {
                    // если клетка уже занята, проверяем совпадение 
                    if (grid[currentRow][checkCol] != '\0' && grid[currentRow][checkCol] != word[i])
                        return false;
                }
            }
            return true;
        }
        // такая же проверка, только теперь вертикального слова
        private bool CanPlaceVertical(string word, (int row, int col, char letter, int wordIndex) intersections)
        {
            int startRow = intersections.row - intersections.wordIndex;
            int endRow = startRow + word.Length - 1;

            // проверка границ 
            if (startRow < 0)
                return false;

            int currentCol = intersections.col;

            // проверяем конфликты
            for (int i = 0; i < word.Length; i++)
            {
                int checkRow = startRow + i;

                if (checkRow < grid.Length && checkRow >= 0)
                {
                    // если строка существует 
                    if (grid[checkRow] != null && currentCol < grid[checkRow].Length)
                    {
                        if (grid[checkRow][currentCol] != '\0' && grid[checkRow][currentCol] != word[i])
                            return false;
                    }
                }
                else { continue; }

            }
            return true;
        }

        // размещ горизонт слова
        private void PlaceHorizontal(string word, (int row, int col, char letter, int wordIndex) intersection)
        {
            int startCol = intersection.col - intersection.wordIndex;
            int currentRow = intersection.row;
            int requiredLenght = startCol + word.Length;

            // расширяем строку, если это нужно
            if (requiredLenght > grid[currentRow].Length)
            {
                Array.Resize(ref grid[currentRow], requiredLenght);
            }

            // заполняем слово 
            for (int i = 0; i < word.Length; i++)
            {
                grid[currentRow][startCol + i] = word[i];
            }
            Console.WriteLine($"Размещено горизонтально: {word}");
            placedWords.Add(new PlacedWord
            {
                Word = word,
                Row = currentRow,
                Col = startCol,
                IsHorizontal = true
            });
        }

        // то же самое размещение, но теперь вертикального слова 
        private void PlaceVertical(string word, (int row, int col, char letter, int wordIndex) intersection)
        {
            int startRow = intersection.row - intersection.wordIndex;
            int currentCol = intersection.col;
            int requiredRows = startRow + word.Length;

            if (requiredRows > grid.Length)
            {
                Array.Resize(ref grid, requiredRows);
            }

            for (int i = 0; i < word.Length; i++)
            {
                int currentRow = startRow + i;

                // Если стркоа не существует, создаём её
                if (grid[currentRow] == null)
                {
                    grid[currentRow] = new char[currentCol + 1];

                }
                // Если столбца не существует, расширяем строку
                else if (currentCol >= grid[currentRow].Length)
                {
                    Array.Resize(ref grid[currentRow], currentCol + 1);
                }
                grid[currentRow][currentCol] = word[i];
            }
            Console.WriteLine($"Размещено вертикально: {word}");
            placedWords.Add(new PlacedWord
            {
                Word = word,
                Row = startRow,
                Col = currentCol,
                IsHorizontal = false
            });
        }


        // Визуализация кроссворда в консоли
        public void PrintCrossword()
        {
            if (grid == null || grid.Length == 0)
            {
                Console.WriteLine("Кроссворд пуст");
                return;
            }

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int maxWidth = grid.Max(row => row?.Length ?? 0);

            Console.WriteLine("\nСгенерированный кроссворд:");

            // Верхняя граница таблицы
            Console.Write("┌");
            for (int j = 0; j < maxWidth; j++)
            {
                Console.Write("───");
                if (j < maxWidth - 1) Console.Write("┬");
            }
            Console.WriteLine("┐");

            // Содержимое таблицы
            for (int i = 0; i < grid.Length; i++)
            {
                Console.Write("│");
                for (int j = 0; j < maxWidth; j++)
                {
                    char cell = (j < grid[i]?.Length) ? grid[i][j] : '\0';
                    string output = (cell == '\0') ? " " : cell.ToString();
                    Console.Write($" {output} │"); // Каждая ячейка в своем блоке
                }
                Console.WriteLine();

                // Разделитель между строками (кроме последней)
                if (i < grid.Length - 1)
                {
                    Console.Write("├");
                    for (int j = 0; j < maxWidth; j++)
                    {
                        Console.Write("───");
                        if (j < maxWidth - 1) Console.Write("┼");
                    }
                    Console.WriteLine("┤");
                }
            }

            // Нижняя граница таблицы
            Console.Write("└");
            for (int j = 0; j < maxWidth; j++)
            {
                Console.Write("───");
                if (j < maxWidth - 1) Console.Write("┴");
            }
            Console.WriteLine("┘");
        }
    }

}
