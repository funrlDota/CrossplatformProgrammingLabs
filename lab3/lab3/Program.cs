﻿using System;
using System.IO;

class Program
{
    static int[] parent;

    static void Main()
    {
        string[] inputLines = File.ReadAllLines("INPUT.TXT");
        int n = int.Parse(inputLines[0]);
        int[,] adjacencyMatrix = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            string[] row = inputLines[i + 1].Split(' ');
            for (int j = 0; j < n; j++)
            {
                adjacencyMatrix[i, j] = int.Parse(row[j]);
            }
        }

        parent = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
        }

        bool isTree = IsTree(adjacencyMatrix, n);

        File.WriteAllText("OUTPUT.TXT", isTree ? "YES" : "NO");
    }

    static int Find(int vertex)
    {
        if (parent[vertex] == vertex)
        {
            return vertex;
        }
        return parent[vertex] = Find(parent[vertex]);
    }

    static bool Union(int u, int v)
    {
        int rootU = Find(u);
        int rootV = Find(v);

        if (rootU == rootV)
        {
            return false;
        }

        parent[rootU] = rootV;
        return true;
    }

    static bool IsTree(int[,] adjacencyMatrix, int n)
    {
        int edgeCount = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (adjacencyMatrix[i, j] == 1)
                {
                    edgeCount++;

                    if (!Union(i, j))
                    {
                        return false;
                    }
                }
            }
        }

        return edgeCount == n - 1;
    }
}