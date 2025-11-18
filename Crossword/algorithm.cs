using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GUI;

namespace Crossword;

public class algorithm
{
    private visualBox menu;
    private Random random;
    private char[][] words;
    private char[,] grid;
    private int rows=50;
    private int cols=50;

    public List<char[]> WordsToPlace = new List<char[]>();
    List<PlaceWord> placeWords = new List<PlaceWord>();
    List<char[]> MissWords = new List<char[]>();

    public algorithm()
    {
        menu = new visualBox();
        random = new Random();
        grid = new char[rows, cols];
        words = menu.WordsArr;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = ' '; // Используем точку как заполнитель
            }
        }
    }
    public void ShowCrossword()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++) 
            {
                Console.Write(grid[i, j]);
            }
            Console.WriteLine();
        }
          
    }
    private void TakeWords()
    {
        if (words.Length > 20) 
        {
            Array.Resize(ref words, 20);
        }
        menu.Sort(words);
        WordsToPlace.AddRange(words);
    }
   
    private void CheckWord(char[] WordToCheck)
    {
        for (int i = 0; i < placeWords.Count; i++) 
        {
            int LetterCounter = 0;
            foreach(var letter in WordToCheck)
            {
                for(int j = 0; j < placeWords[i].Word.Length;j++)
                {
                    int CurrentX;
                    int CurrentY;
                    if (letter == placeWords[i].Word[j])
                    {

                        if (placeWords[i].IsHorisontal)
                        {
                            CurrentX = placeWords[i].StartX + j;
                            CurrentY = placeWords[i].StartY;


                        }
                        else
                        {
                            CurrentX = placeWords[i].StartX;
                            CurrentY = placeWords[i].StartY + j;
                        }

                        if (CurrentX >= 0 && CurrentY >= 0 && CurrentX < 50 && CurrentY < 50)
                        {
                            if (placeWords[i].IsHorisontal) 
                            {

                            }
                        }
                        else { continue; }
                        
                    }
                }
                LetterCounter++;
            }
        }
    }
    public void GenerationCros()
    {
        TakeWords();
        int FirstStartX = (cols - WordsToPlace[0].Length) / 2;
        int FirstStartY = rows / 2;
        for (int i = 0; i < WordsToPlace[0].Length; i++)
        {
            grid[FirstStartY, FirstStartX + i] = WordsToPlace[0][i];
        }
        PlaceWord NewPlaceWord = new PlaceWord
        {
            Word = WordsToPlace[0],
            StartY = FirstStartY,
            StartX = FirstStartX,
            IsHorisontal = true
        };
        placeWords.Add(NewPlaceWord);
        WordsToPlace.RemoveAt(0);
        while (WordsToPlace.Count != 0)
        {
            for (int i = 0; i < WordsToPlace.Count; i++) 
            {
                char[] WordToTry = WordsToPlace[i];

            }
        }

        ShowCrossword();
    }

}

public class PlaceWord
{
    public char[] Word { get; set; } 
    public int StartX {  get; set; }
    public int StartY { get; set; }
    public bool IsHorisontal {  get; set; }

}

    // Основной метод генерации массива
    

