using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lista_2
{
    class Lista_2AB
    {
        
        static void Main(string[] args)
        {
            char[] polskieZnaki ={'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'ń', 'n', 'o', 'ó', 'p', 'r', 's',
                'ś','t','u','w','y','z','ź','ż'};
            Random rand = new Random();
            List<string> wordList = new List<string>();
            string word = "";
            
            
            while (wordList.Count<200)
            {
                int index;
                word = "";
                for (int i = 0; i < 4; i++)
                {
                    index = rand.Next(0, 32);
                    word += polskieZnaki[index];
                }
                if(CheckWord(word, wordList))
                    wordList.Add(word);
            }

            PrintWords(wordList);
            WriteLine("\n\n");
            WriteLine("__________Przykład B________");

            string text = File.ReadAllText("4wyrazy.txt");
            Dictionary<char, int> listOfCharacters = new Dictionary<char, int>();
            Dictionary<char, double> listOfModelProbability = new Dictionary<char, double>();
            text = text.ToLower();

            listOfCharacters = CheckList(text, listOfCharacters);
            listOfModelProbability = SetProbability(listOfCharacters,listOfModelProbability);
            PrintListOfModelProbability(listOfModelProbability);

            wordList.Clear();
            while (wordList.Count < 200)
            {
                word = "";
                for (int i = 0; i < 4; i++)
                {

                    double random =  rand.NextDouble();
                    double value = GetValue(random, listOfModelProbability);
                    word += listOfModelProbability.First(x => x.Value == value).Key;
                }
                if (CheckWord(word, wordList))
                    wordList.Add(word);
            }
            PrintWords(wordList);
            ReadKey();
        }

        private static void PrintWords(List<string> wordList)
        {
            int k = 1;
            foreach (string item in wordList)
            {
                WriteLine($"{k}  " + item);
                ++k;
            }
        }

        private static Dictionary<char, double> SetProbability(Dictionary<char, int> listOfCharacters, Dictionary<char, double> listOfModelProbability)
        {
            
            int countAllElements=listOfCharacters.Sum(x=>x.Value);
            double probability;
            foreach (var item in listOfCharacters)
            {
                probability = (double)item.Value / countAllElements;
                listOfModelProbability.Add(item.Key,probability);

            }
            return listOfModelProbability;
        }

        private static double GetValue(double random, Dictionary<char, double> listOfModelProbability)
        {
            List<double> listOfProbability = new List<double>();
            
            foreach (var item in listOfModelProbability)
            {
                listOfProbability.Add(item.Value);
            }
            double summary = 0;
            for (int i = 0; i <listOfProbability.Count; i++)
            {
                summary += listOfProbability[i];
                if ( summary> random)
                    return listOfProbability[i];
            }
            return summary;
        }

        private static void PrintListOfModelProbability(Dictionary<char, double> listOfProbality)
        {
            WriteLine("\n\n\n------ Lista");
            foreach (var item in listOfProbality)
            {
               WriteLine($"{item.Key}" + " - " + item.Value);
            }
        }

        private static Dictionary<char,int> CheckList(string text, Dictionary<char, int> listOfCharacters)
        {
            foreach (char sign in text)
            {
                
                if (listOfCharacters.ContainsKey(sign))
                {
                    if (listOfCharacters.ContainsKey(sign))
                        listOfCharacters[sign] += 1;
                    else
                        listOfCharacters.Add(sign, 1);
                }
                else
                  listOfCharacters.Add(sign, 1);
            }
            return listOfCharacters= SortAndRemove(listOfCharacters);
        }

        private static Dictionary<char,int> SortAndRemove(Dictionary<char, int> listOfCharacters)
        {
            listOfCharacters.Remove('\n');
            listOfCharacters = listOfCharacters.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value); //sortowanie według ilości powtórzeń..
            listOfCharacters.Remove(listOfCharacters.Keys.Last());
            return listOfCharacters;
        }

        private static bool CheckWord(string word,List<string> list)
        {
            if (!list.Contains(word))
                return true;
            return false;
        }
    }
    
}
