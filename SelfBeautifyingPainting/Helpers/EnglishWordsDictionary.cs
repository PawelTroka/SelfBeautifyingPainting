using System;
using System.Collections.Generic;
using System.IO;

namespace SelfBeautifyingPainting.Helpers
{
    public static class EnglishWordsDictionary
    {
        private static readonly Random random = new Random();

        private static List<string> dictionary;

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

        private static void InitDictionary()
        {
            dictionary = new List<string>();
            dictionary.AddRange(File.ReadAllLines("dictionary.txt"));
        }
    }
}