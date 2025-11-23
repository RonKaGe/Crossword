using System.Text.RegularExpressions;       // библиотека для проверки на русские буквы
using System.Xml.Serialization;
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
				Console.WriteLine("3. Очистить все");
				Console.WriteLine("4. Вывести список слов");
				Console.WriteLine("5. Вывести Кроссворд");
				Console.WriteLine("6. Выход");

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
						case 3:
							ClearAll();
							DataWords.WriteToFile(WordsArr);
							break;
						case 4:
							ShowArr();
							break;
						case 5:
							WaitingForButton();
							break;
						case 6:
							EndOfProgramm = true;
							break;
						default:
							Console.WriteLine("Ошибка ввода");
							WaitingForButton();
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
			WaitingForButton();
        }
	
	private void DeleteLastWord()													// удаление последнего слова из массива
		{
			Array.Resize(ref WordsArr, WordsArr.Length - 1);
        }

			
		
	private void ClearAll()
		{
			Array.Resize(ref WordsArr, 0);

		}
        public void ShowData()
        {
            foreach (char[] p in WordsArr)
            {
                Console.WriteLine(p);
            }
        }
		private void ShowArr()
		{
			foreach(char[] p in WordsArr)
			{
				Console.WriteLine(p);
			}
			WaitingForButton();

		}
		private void WaitingForButton()
		{
			Console.WriteLine("Нажмите на любую клавишу...");
			Console.ReadKey();
		}
		public List<string> ListOfWords()
		{
			List<string> list = new List<string>();
			for(int i = 0;i < WordsArr.Length; i++)
			{
                list.Add(new string(WordsArr[i]));
			}
			return list;
		}

	}
}