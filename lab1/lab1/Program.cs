using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string inputFileName = GetInputFilePath();
        string outputFileName = GetOutputFilePath();

        // Зчитуємо слово з вхідного файлу
        string word = File.ReadAllText(inputFileName).Trim();

        // Обчислюємо кількість різних слів та записуємо її у вихідний файл
        long totalWords = CountDistinctWords(word);
        File.WriteAllText(outputFileName, totalWords.ToString());
    }

    static string GetInputFilePath()
    {
        Console.WriteLine("Введите путь к входному файлу:");
        return Console.ReadLine().Trim();
    }

    static string GetOutputFilePath()
    {
        Console.WriteLine("Введите путь к выходному файлу:");
        return Console.ReadLine().Trim();
    }

    static long CountDistinctWords(string word)
    {
        Dictionary<char, int> letterCounts = new Dictionary<char, int>();

        // Рахуємо кількість повторень кожної літери в слові
        foreach (char letter in word)
        {
            if (letterCounts.ContainsKey(letter))
                letterCounts[letter]++;
            else
                letterCounts[letter] = 1;
        }

        long totalWords = Factorial(word.Length);

        // Ділимо загальну кількість можливих перестановок на факторіали повторень кожної літери
        foreach (int count in letterCounts.Values)
        {
            totalWords /= Factorial(count);
        }

        return totalWords;
    }

    static long Factorial(int n)
    {
        long result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }
}