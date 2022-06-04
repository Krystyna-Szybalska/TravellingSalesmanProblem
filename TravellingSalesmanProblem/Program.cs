using System;

namespace TravellingSalesmanProblem
{
    class Program
    {
        //komiwojażer musi odwiedzić wszystkie miasta dokładnie raz
        //wybór najtańszej ścieżki
        //rozwiazanie optymalne jest czasowo nierealistyczne
        //szukamy rozwiazań suboptymalnych
        //algorytm ewolucyjny - tworzymy osobnika rodzica, w oparciu o niego modyfikujemy dziecko, które staje się rodzicem jeśli jest lepsze od rodzica
        //RODZIC: jednowymiarowa tablica mająca N (liczba miast) komórek, w komórce liczby od 1-100 (w której kolejności dane miasto zostanie odwiedzone)
        //DZIECKO poddajemy inwersji: losujemy dwie liczby z zakresu 1 do 100, podmieniamy te liczby w danych miastach
        //jeśli koszt podróży jest mniejszy, dziecko staje się rodzicem dla kolejnego pokolenia, jeśli nie dziecko xx
        // założenie: czas, liczba pokoleń albo zrobić licznik zliczająćy nieudane próby udanego dziecka
        // np. jeśli po 10000 prób nie udalo się uzyskać niczego lepszego niż obecne dziecko koniec
        // N = 100. wypełnione liczbami losowymi, ni emusi być symetryczne, koszt 5-50, dodatni, naturalny
        // output: kolejność miast, najtańsza scieżka

        static void Main(string[] args)
        {
            int[,] matrix = CreateMatrix(100);
            int[] parent = new int[100];
          
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = i;
            }

            int youAreAFAILURECounter = 0;
            int parentCost=0;
            while (youAreAFAILURECounter<100)
            {
                parentCost = CountCost(matrix, parent);
                int[] child = CreateChild(parent);
                int childCost = CountCost(matrix, child);
                if (parentCost > childCost)
                {
                    parent = child;
                    parentCost = childCost;
                    youAreAFAILURECounter = 0;
                }
                else youAreAFAILURECounter++;
            }

            Console.WriteLine("Optymalna ścieżka to ");
            foreach (var item in parent)
            {
                Console.Write(item+" -> ");
            }
            Console.WriteLine($"Koszt tej ścieżki to {parentCost}");

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
