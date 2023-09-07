using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string word = Console.ReadLine();

        Dictionary<char, int> letterCount = new Dictionary<char, int>();
        foreach (char letter in word)
        {
            if (letterCount.ContainsKey(letter))
            {
                letterCount[letter]++;
            }
            else
            {
                letterCount[letter] = 1;
            }
        }

        double result = Factorial(word.Length);
        foreach (int count in letterCount.Values)
        {
            result /= Factorial(count);
        }

        Console.WriteLine(result);
    }

    static double Factorial(int n)
    {
        double factorial = 1;
        for (int i = 1; i <= n; i++)
        {
            factorial *= i;
        }
        return factorial;
    }
}