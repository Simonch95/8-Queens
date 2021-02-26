using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Kodowanie_Lista1
{
    
   public class Lista1
    {
        private static List<int> countList = new List<int>();
        private static List<char> letterList = new List<char>();

        static void Main(string[] args)
        {

            string text = File.ReadAllText("Literki.txt");
            if (text != null && text != "")
            {
                WriteLine(text);
                CheckList(text);
                PrintLetters();
                Write("Czy wyliczyć entropię? t/n  ");
                char sign = Convert.ToChar(ReadLine());
                if (sign == 't' || sign == 'T')
                    CalculateEntropia();
            }
            ReadKey();
        }

        private static void CalculateEntropia()
        {
            int score = 0;
            foreach (int item in countList)
                score += item;
            double prob = 0;
            double Entropia = 0f;
            foreach (int item in countList)
            {

                prob = (double)item / score;
                Entropia += -prob * Math.Log(prob, 2);

            }
            WriteLine(Entropia);
        }
        private static void PrintLetters()
        {
            int indexLitery = 0;
            int suma = 0;
            foreach (int zmienn in countList)
            {
                suma += zmienn;
            }
            foreach (int ilos in countList)
            {
                WriteLine($"{letterList[indexLitery]} - {ilos} - "+$"{(float)ilos/suma}");
                ++indexLitery;
            }
        }

        private static void CheckList(string text)
        {
            foreach (char sign in text)
            {
                if (!letterList.Contains(sign)) 
                {
                    letterList.Add(sign);
                    countList.Add(1);
                }
                else
                {
                    int index = letterList.IndexOf(sign);
                    int newCount = countList[index];
                    ++newCount;
                    countList.RemoveAt(index);
                    countList.Insert(index, newCount);
                }
            }
        }
    }
}
