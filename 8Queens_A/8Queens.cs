using System;
using System.Collections.Generic;
using System.Text;
using  static System.Console;
//Szymon Chłond
//Grupa 5
//Zadanie 2. Napisz program sprawdzający, które permutacje 8 cyfr są rozwiązaniem problemu 8-hetmanów tj. sprawdź dla każdej permutacji wygenerowanej w zad. 1 czy jest ona rozwiązaniem,
//wyświetl wszystkie permutacje będące rozwiązaniami [O]. Przedstaw rozwiązania graficznie na szachownicy. Spróbuj też znaleźć rozwiązania problemu n-hetmanów (n = 4,...,1).
namespace _8Queens_A
{
    class formPermut
    {
        public void SwapTwoNumber(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public List<string> Permut(int[] list, int k, int m, List<string> permutList)
        {
            int i;
            if (k == m)
            {
                string perm = "";
                for (i = 0; i <= m; i++)
                    perm += Convert.ToString(list[i]);
                
                permutList.Add(perm);
                perm = "";
            }
            else
                for (i = k; i <= m; i++)
                {
                    SwapTwoNumber(ref list[k], ref list[i]);
                    Permut(list, k + 1, m, permutList);
                    SwapTwoNumber(ref list[k], ref list[i]);
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
            List<string> permutList = new List<string>();
            List<string> goodPermutation = new List<string>();
     
            Console.Write("Podaj rozmiar permutacji : ");
             n = Convert.ToInt32(Console.ReadLine());
            int[] arr1 = new int[n];

            for (int i = 0; i < n; i++)
                arr1[i] = i;

            Write($"\nWyniki permutacji o rozmiarze {n} cyfr : \n");

            permutList = test.Permut(arr1, 0, n - 1, permutList);
            WriteLine();
            WriteLine("_________________________________________________________");

            //foreach (string number in permutList) 
            //    Write(number + " ");

            goodPermutation = TakeOnePermutation(permutList,goodPermutation);
            Write("\n\n");
            Write("Ilosc dobrych rozwiazan: ");
            WriteLine($"{goodPermutation.Count}");
            WriteLine("Czy mam narysować szchownice? (-t/Tak, -n/Nie)");
            string p = ReadLine();

            if (p == "t" || p=="T")
            {
                foreach (string item in goodPermutation)
                {
                  SetQueensOnBoard(item);
                  WriteLine("\n_____________________________________________\n");
                }
            }
            WriteLine("Lista dobrych rozwiazan. Oto one:");
            foreach (string item in goodPermutation)
            {
                WriteLine(item);
            }

        }

        #region Drukowanie Tablicy
        private static void SetQueensOnBoard(string item)
        {
            int j = 0;
            char[,] board=new char[item.Length,item.Length];
            for (int i = 0; i < item.Length; i++)
            { 
                j = Convert.ToInt32(Convert.ToString(item[i])); 
                board[j, i] ='Q';
            }

            PrintBoard(board, item.Length);
        }

        private static void PrintBoard(char[,] board, int size)
        {
            DrawUnderscore(size);
            WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Write(board[i,j]+"|");
                }
                WriteLine();
                if (i < size - 1)
                {
                //    for (int j = 0; j < size; j++)
                //    {
                //        Write("--");
                //    }

                //    WriteLine();
                }else DrawMacron(size);
            }
            
        }

        private static void DrawMacron(int size)
        {
            OutputEncoding = Encoding.Unicode;
            for (int i = 0; i < size; i++)
            {
                Write("\x035e"+ "\x035e");
            }
        }
        private static void DrawUnderscore(int size)
        {
           
            for (int i = 0; i < size; i++)
            {
                Write("__");
            }
        }
#endregion

        #region lab2

        public static string CheckPermutation(string permutation)
        {
            int size = permutation.Length;
            int queen = 0;
            int[,] arr = new int[size, size];
            string goodPermutation = "";

            while (queen < size)
            {
                int row = Convert.ToInt32(Convert.ToString(permutation[queen]));
                if (CheckPlace(ref arr, queen, row))
                {
                    InsertArray(ref arr, queen, permutation);
                    goodPermutation += Convert.ToString(permutation[queen]);
                    ++queen;
                }
                else
                {
                    ClearArray(ref arr, size);
                    return "";
                }
            }

            ClearArray(ref arr,size);

            return goodPermutation;
        }

        private static void ClearArray(ref int[,] arr,int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    arr[i, j] = 0;
                }
            }
        }

        private static bool CheckPlace(ref int[,] arr, int queen,int row)
        {
            if (arr[row,queen] == 0)
                return true;

            return false;
        }

        public static List<string> TakeOnePermutation(List<string> permutationList,List<string>goodLst)
        {
            string number = "";
            foreach (string permutation in permutationList)
            {
                number=CheckPermutation(permutation);
                if (number!="")
                {
                    goodLst.Add(number);
                }

            }
            return goodLst;
        }

        public static int[,] InsertArray(ref int[,]arr,int k,string permutation)
        {
            int size = permutation.Length;
            int row = Convert.ToInt32(Convert.ToString(permutation[k]));
            int col = k;

            for (int j = 0; j < size; j++) // wypełnij wiersz
                arr[row, j] = 1;


            for (int j = 0; j < size; j++) // wypełnij kolumne
                arr[j, k] = 1;

            while (row != size && col!=size)// przekątna w dol
            {
                arr[row, col] = 1;
                ++row;
                col++;
            }

            row = Convert.ToInt32(Convert.ToString(permutation[k]));
            col = k;

            while (row > -1 && col!=size) // przekątna do gory
            {
                arr[row, col] = 1;
                if (row == -1)
                    break;

                --row;
                ++col;
            }

            return arr;
        }

        #endregion

    }
}


