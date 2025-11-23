using System.Text;
namespace DataBase
{

    class Words
    {

        private string FilePath = "Words.txt";
        public char[][] JaggedCharArr;
        public void AddStartWords() // ������� �� ���������� ��������� ����
        {
            File.AppendAllText(FilePath, "�����\n���\n�����������\n����\n�����\n���\n����������\n����\n������\n��������\n���\n�����\n���������\n����\n�����\n");

        }
        public Words()                                                          // ����������� ������(������ ���� � ��������� ������ ������� �� �����)
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
                    Console.WriteLine("���� ����");
                    return;
                }
            }

            catch (FileNotFoundException)                                       // ��������� ������
            {
                File.Create(FilePath).Close();
                AddStartWords();
                return;
            }



        }


        public void ShowData()                                                  //����������� ������� 
        {
            foreach (char[] p in JaggedCharArr)
            {
                Console.WriteLine(p);
            }
        }
        public void WriteToFile(char[][] WordsArr)                             //������ � ���� (��� ���������� ������ ��������� � ������)
        {
            JaggedCharArr = WordsArr;
            string[] word = new string[JaggedCharArr.Length];
            int i = 0;
            foreach (char[] p in JaggedCharArr)
            {
                string currentWord = new string(p);
                currentWord.ToUpper();
                word[i++] = currentWord;
            }
            File.WriteAllLines(FilePath, word);

        }
    }
}