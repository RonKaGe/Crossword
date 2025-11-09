using System.Text;
namespace DataBase
{

    class Words
    {
        private string FilePath = "C:\\Users\\Zhuck\\Documents\\Visual Studio 2022\\Crossword\\Crossword\\Words.txt";
        public char[][] JaggedCharArr;
        public Words()                                                          // конструктор класса(читает файл и заполняет массив данными из файла)
        {
            int numline = File.ReadLines(FilePath).Count();
            this.JaggedCharArr = new char[numline][];
            int i = 0;
            try
            {
                string[] Lines = File.ReadAllLines(FilePath,Encoding.UTF8);
                if (Lines.Length > 0) 
                {
                    foreach(string Line in Lines)
                    {
                        JaggedCharArr[i++] = Line.ToCharArray();
                    }
                }
                else
                {
                    Console.WriteLine("Файл Пуст");
                    return;
                }
            }

            catch (FileNotFoundException)                                       // обработка ошибок
            {
                Console.WriteLine("Файл не найден"); return;
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
