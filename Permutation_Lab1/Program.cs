using System;
using System.Collections.Generic;
using System.Diagnostics;
// Szymon Chłond
// Grupa 5
// Zadanie 1
// Napisz program implementujący algorytm generujący wszystkie permutacje n cyfr 
// (nie można korzystać z gotowych funkcji generujących permutacje) w następujący sposób:
// aby wygenerować permutacje program musi pamiętać permutacje z poprzednich kroków(metoda rekurencyjna) [O]

namespace Permutation_Lab1
{
    class formPermut
    {
        public void swapTwoNumber(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public List<string> prnPermut(int[] list, int k, int m,List<string>permutList)
        {
            int i;
            string perm = "";
            if (k == m)
            {
               
                for (i = 0; i <= m; i++)
                {
                    perm += Convert.ToString(list[i]);
                }

                permutList.Add(perm);
                perm = "";
            }
            else
                for (i = k; i <= m; i++)
                {
                    swapTwoNumber(ref list[k], ref list[i]);
                    prnPermut(list, k + 1, m,permutList);
                    swapTwoNumber(ref list[k], ref list[i]);
                }

            return permutList;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            formPermut test = new formPermut();
            List<string>permutList=new List<string>();
         
            Console.Write("Podaj rozmiar permutacji : ");
            n = Convert.ToInt32(Console.ReadLine());
            int[] arr1 = new int[n];
            for (int i = 0; i < n; i++)
               arr1[i] = i;

            permutList = test.prnPermut(arr1, 0, n - 1, permutList);
            Console.WriteLine("\nWyniki rozwiazania:\n");
            foreach (string number in permutList)
            {
               Console.WriteLine(number);
            }
            Console.Write("\n\n");
        }
    }
}
