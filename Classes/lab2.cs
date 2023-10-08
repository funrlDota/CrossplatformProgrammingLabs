using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class lab2
    {
        public static void Run(string inputFilePath, string outputFilePath)
        {
            string[] input = File.ReadAllLines(inputFilePath);
            string[] nk = input[0].Split();
            int N = int.Parse(nk[0]);
            int K = int.Parse(nk[1]);

            char[,] maze = new char[N, N];
            for (int i = 0; i < N; i++)
            {
                string line = input[i + 1];
                for (int j = 0; j < N; j++)
                {
                    maze[i, j] = line[j];
                }
            }

            long[,] dp = new long[N, N];
            dp[0, 0] = 1;

            for (int k = 1; k <= K; k++)
            {
                long[,] newDP = new long[N, N];

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (maze[i, j] == '0')
                        {
                            foreach (int[] dir in new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 } })
                            {
                                int ni = i + dir[0];
                                int nj = j + dir[1];

                                if (ni >= 0 && ni < N && nj >= 0 && nj < N)
                                {
                                    newDP[ni, nj] += dp[i, j];
                                }
                            }
                        }
                    }
                }

                dp = newDP;
            }

            long result = dp[N - 1, N - 1];
            File.WriteAllText(outputFilePath, result.ToString());
        }
    }
}
