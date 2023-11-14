namespace Lab5Classes
{
    public class lab1
    {
        public static string Run(string msg)
        {
            string inputFileName = "1.txt";
            File.WriteAllText(inputFileName, msg);
            // Зчитуємо слово з вхідного файлу
            string word = File.ReadAllText(inputFileName).Trim();

            // Обчислюємо кількість різних слів та записуємо її у вихідний файл
            long totalWords = CountDistinctWords(word);

            return totalWords.ToString();
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
}