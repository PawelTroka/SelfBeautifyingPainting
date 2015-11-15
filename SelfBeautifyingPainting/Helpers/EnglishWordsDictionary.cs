using System;
using System.Collections.Generic;
using System.IO;

namespace SelfBeautifyingPainting.Helpers
{
    public static class EnglishWordsDictionary
    {
        private static Random random = new Random();
        public static List<string> Dictionary
        {
            get
            {
                if (dictionary == null)
                    InitDictionary();

                return dictionary;
            }
        }

        public static string GetRandomWord()
        {
            return Dictionary[random.Next(0, Dictionary.Count)];
        }

        private static List<string> dictionary;

        private static void InitDictionary()
        {
            dictionary = new List<string>();
            dictionary.AddRange(File.ReadAllLines("dictionary.txt"));
        }
    }
}