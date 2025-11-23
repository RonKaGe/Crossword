using System.Text;
namespace DataBase
{

    class Words
    {

        private string FilePath = "Words.txt";
        public char[][] JaggedCharArr;
        public void AddStartWords() // костыль на добавление начальных слов
        {
            File.AppendAllText(FilePath, "АБРИС\nЯМА\nВДОХНОВЕНИЕ\nЩЕЛЬ\nГАРАЖ\nЭРА\nБИБЛИОТЕКА\nОВОД\nЖЕМЧУГ\nАКВАРИУМ\nУХО\nДОЗОР\nВЕЛОСИПЕД\nИНЕЙ\nАВТОР\n");

        }
        public Words()                                                          // конструктор класса(читает файл и заполняет массив данными из файла)
        {
            int i = 0;
            try
            {
                string[] Lines = File.ReadAllLines(FilePath, Encoding.UTF8);
                JaggedCharArr = new char[Lines.Length][];
                if (Lines.Length > 0)
                {
                    foreach (string Line in Lines)
                    {
                        JaggedCharArr[i++] = Line.ToCharArray();
                    }
                }
                else
                {
                    Console.WriteLine("Файл Пуст, вводим базовые слова");
                    AddStartWords();
                    
                }
            }

            catch (FileNotFoundException)                                       // обработка ошибок
            {
                File.Create(FilePath).Close();
                AddStartWords();
                return;
            }



        }


        public void ShowData()                                                  //отображение массива 
        {
            foreach(char[] p in JaggedCharArr)
            {
                Console.WriteLine(p);
            }
        }
    public void WriteToFile(char [][] WordsArr)                             //запись в файл (для сохранения данных внесенных в массив)
        {
            JaggedCharArr = WordsArr;
            string[] word = new string[JaggedCharArr.Length];
            int i = 0;
            foreach(char [] p in JaggedCharArr)
            {
                string currentWord = new string(p);
                currentWord.ToUpper();
                word[i++] = currentWord;
            }
            File.WriteAllLines(FilePath,word);

        }
    }
}