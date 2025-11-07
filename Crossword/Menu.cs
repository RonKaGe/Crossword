using System.Text.RegularExpressions;		// библиотека для проверки на русские буквы

namespace GUI
{
	public class visualBox

	{
		
		private System.Text.Encoding encoding = System.Text.Encoding.UTF8;		// чтобы не было проблем с кодировкой
																				//(возможно лишнее, но я не стал удалять)
        private int choice;
		private bool EndOfProgramm = false;
		private string? AppendWord;
        string pattern = @"^[а-яА-ЯёЁ]+$"; // условие проверки на русские буквы
		private string FilePath = "C:\\Users\\Zhuck\\Documents\\Visual Studio 2022\\Crossword\\Crossword\\Words.txt";

		public void ShowMenu()
		{
			while (EndOfProgramm == false)
			{
				Console.Clear();
				Console.WriteLine("1. Добавить слово");
				Console.WriteLine("2. Удалить последнее слово");
				Console.WriteLine("3. Удалить слово по индексу");
				Console.WriteLine("4. Очистить все");
				Console.WriteLine("5. Вывести список слов");
				Console.WriteLine("6. Вывести Кроссворд");
				Console.WriteLine("7. Выход");

				Console.Write("Выберите пункт меню: ");

				if (int.TryParse(Console.ReadLine(), out choice))
				{
					switch (choice)
					{
						case 1:
							AddWord();
							break;
					}
				}


			}
		}
	private void AddWord()
		{
			Console.WriteLine("Введите слово: ");
			AppendWord = Console.ReadLine();
			if (AppendWord != null && Regex.IsMatch(AppendWord, pattern))
			{
				File.AppendAllText(FilePath, AppendWord.ToUpper()+Environment.NewLine, encoding); // добавляем слово в конец с переходом на новую строку 
                Console.WriteLine("Слово успешно добавлено");

            }
            else
			{
				Console.WriteLine("Ошибка ввода слова!");
			}
			Console.WriteLine("Нажмите на любую клавишу, чтобы продолжить...");
			Console.ReadKey();
		}

	public void AddStartWords() // костыль на добавление начальных слов, в будущем возможно уберу (в данный момент нигде не используется)
		{
			File.AppendAllText(FilePath,"АБРИС\nЯМА\nВДОХНОВЕНИЕ\nЩЕЛЬ\nГАРАЖ\nЭРА\nБИБЛИОТЕКА\nОВОД\nЖЕМЧУГ\nАКВАРИУМ\nУХО\nДОЗОР\nВЕЛОСИПЕД\nИНЕЙ\nАВТОР\n");

        }
	}
}