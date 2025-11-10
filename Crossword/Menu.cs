using System.Text.RegularExpressions;       // библиотека для проверки на русские буквы
using DataBase;
namespace GUI
{
	public class visualBox

	{
		private Words DataWords = new Words();
		private char[][] WordsArr;
		public visualBox()
		{
			WordsArr = DataWords.JaggedCharArr;
		}
        private int choice;
		private bool EndOfProgramm = false;
        string pattern = @"^[а-яА-ЯёЁ]+$"; // условие проверки на русские буквы
		private string FilePath = "Words.txt";

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
					switch (choice)											//недоработанный еще выбор пункта меню
					{
						case 1:
							AddWord();
							DataWords.WriteToFile(WordsArr);
							break;
						case 2:
							DeleteLastWord();
                            DataWords.WriteToFile(WordsArr);
                            break;

					}
				}


			}
		}
	private void AddWord()																//добавление слова в конец массива
		{
			string AppendWord = null;
            while (AppendWord == null || !Regex.IsMatch(AppendWord, pattern))  
			{
                Console.WriteLine("Введите новое слово: ");
                AppendWord = Console.ReadLine();

            }
            Array.Resize(ref WordsArr, WordsArr.Length + 1);
			WordsArr[WordsArr.Length - 1]=AppendWord.ToUpper().ToCharArray();
			DataWords.WriteToFile(WordsArr);
			Console.ReadKey();
        }
	private void DeleteLastWord()													// удаление последнего слова из массива
		{
			Array.Resize(ref WordsArr, WordsArr.Length - 1);
        }
	private void SwapArr(char[][] WordsArr, int FirstIndex, int SecondIndex )
		{
			char[] Temp = WordsArr[FirstIndex];
			WordsArr[FirstIndex] = WordsArr[SecondIndex];
			WordsArr[SecondIndex] = Temp;
			
		}
        public void ShowData()
        {
            foreach (char[] p in WordsArr)
            {
                Console.WriteLine(p);
            }
        }
        void Sort()																	//сортируем слова по их размеру 
		{
			for (int i = 0; i < WordsArr.Length - 1; i++)
			{
				for (int j = 0; j < WordsArr.Length - i - 1; j++)
				{
					if (WordsArr[j].Length < WordsArr[j+1].Length)
					{
						SwapArr(WordsArr, j, j+1);
					}
				}
			}
		}
       
        public void AddStartWords() // костыль на добавление начальных слов, в будущем возможно уберу (в данный момент нигде не используется)
		{
			File.AppendAllText(FilePath,"АБРИС\nЯМА\nВДОХНОВЕНИЕ\nЩЕЛЬ\nГАРАЖ\nЭРА\nБИБЛИОТЕКА\nОВОД\nЖЕМЧУГ\nАКВАРИУМ\nУХО\nДОЗОР\nВЕЛОСИПЕД\nИНЕЙ\nАВТОР\n");

        }
	}
}