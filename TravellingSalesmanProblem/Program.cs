using System;
using System.Collections.Generic;

namespace TravellingSalesmanProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = CreateMatrix(100);
            int[] parent = new int[100];
          
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = i;
            }

            int failureCounter = 0;
            int parentCost=0;
            List<int> costInTime = new List<int>();
            while (failureCounter<100)
            {
                parentCost = CountCost(matrix, parent);
                int[] child = CreateChild(parent);
                int childCost = CountCost(matrix, child);
                if (parentCost > childCost)
                {
                    parent = child;
                    parentCost = childCost;
                    failureCounter = 0;
                }
                else failureCounter++;
                costInTime.Add(parentCost);
            }

            Console.WriteLine("Optymalna ścieżka to ");
            foreach (var item in parent)
            {
                Console.Write(item+" - ");
            }
            Console.WriteLine($"Koszt tej ścieżki to {parentCost}");
            
            Console.WriteLine();
            Console.WriteLine("Zmiany kosztu z iteracji na iterację (w nawiasie liczba powtórzeń danej wartości): ");
            
            int howManyTimes = 0;
            for (int i = 0; i < costInTime.Count; i++)
            {
                if (i<costInTime.Count-1 && (costInTime[i] == costInTime[i + 1])) howManyTimes++;
                else{
                    Console.Write($"{costInTime[i]} ({howManyTimes}) >> ");
                    howManyTimes = 0;
                }
                if (i == costInTime.Count-1) Console.Write($"{costInTime[i]}");

            }
        }

        private static void printTable(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write(item+1 + " -> ");
            }
        }

        private static int[] CreateChild(int[] parent)
        {
            int[] newChild = new int[parent.Length];
            for (int n = 0; n < parent.Length; n++)
            {
                newChild[n] = parent[n];
            }

            Random rnd = new Random();
            int i = rnd.Next(parent.Length);
            int j = rnd.Next(parent.Length);
            newChild[i] = parent[j];
            newChild[j] = parent[i];

            return newChild;
        }

        private static int CountCost(int[,] matrix, int[] parent)
        {
            int cost = 0;
            for (int i = 0; i < parent.Length-1; i++)
            {
                cost += matrix[parent[i], parent[i + 1]];
            }

            return cost;
        }

        private static int[,] CreateMatrix(int v)
        {
            int[,] matrix = new int[v,v];

            Random rnd = new Random();
            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    matrix[i,j] = rnd.Next(50);
                }
            }

            for (int i = 0; i < v; i++)
            {
                matrix[i, i] = 0;
            }

            return matrix;
        }
    }
}
