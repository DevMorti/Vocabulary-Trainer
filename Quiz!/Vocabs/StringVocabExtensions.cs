using System;
using System.Collections.Generic;
using System.Linq;

namespace Vokabeltrainer.Vocabs
{
    internal static class StringVocabExtensions
    {
        internal static string[] LettersBetween(this string word, char first, char last)
        {
            List<string> thingsBetween = new List<string>();
            int firstIndex = 0;
            int lastIndex = 0;
            for (int i = 0; i < word.Count(character => character == '('); i++)
            {
                try
                {
                    firstIndex = word.IndexOf('(', lastIndex);
                    lastIndex = word.IndexOf(')', firstIndex);
                    if(lastIndex - firstIndex - 1 >= 0 && firstIndex + 1 >= 0)
                        thingsBetween.Add(word.Substring(firstIndex + 1, lastIndex - firstIndex - 1));
                }
                catch(Exception)
                {
                    continue;
                }
            }

            return thingsBetween.ToArray();
        }

        internal static string FormatVocabAnswer(this string word)
        {
            word = word.RemoveLettersIn('(', ')');
            word = word.ToLower();
            word = word.RemoveSymbols();
            return word;
        }

        internal static string RemoveSymbols(this string word)
        {
            string tempWord = null;
            foreach (char c in word)
            {
                if (char.IsLetter(c))
                    tempWord += c;
            }
            return tempWord;
        }

        internal static string RemoveLettersIn(this string word, char first, char last)
        {
            foreach (string letterInBrackets in word.LettersBetween(first, last))
                word = word.Replace(first + letterInBrackets + last, string.Empty);
            word = word.Trim(new char[] { ' ' });
            return word;
        }

        internal static bool IsVocabString(this string vocab)
        {
            int countedSplits = vocab.Where(character => character == '&').Count();
            if ((countedSplits == 1 || countedSplits == 2) && !vocab.StartsWith("&") && !vocab.EndsWith("&") && !vocab.Contains("&&"))
                return true;
            return false;
        }
    }
}
