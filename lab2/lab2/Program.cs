using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines("INPUT.TXT");
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
        File.WriteAllText("OUTPUT.TXT", result.ToString());
    }
}