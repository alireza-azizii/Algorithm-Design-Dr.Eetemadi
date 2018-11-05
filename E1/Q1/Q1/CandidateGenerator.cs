using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public static class CandidateGenerator
    {
        public static readonly char[] Alphabet =
            Enumerable.Range('a', 'z' - 'a' + 1)
                      .Select(c => (char)c)
                      .ToArray();

        public static string[] GetCandidates(string word)
        {
            List<string> candidates = new List<string>();
            for (int i = 0; i < word.Length+1; i++)
            {
                foreach (var item in Alphabet)
                {
                    var a = Insert(word, i, item);
                    candidates.Add(a);
                }
                
            }
            for (int i = 0; i < word.Length; i++)
            {
                foreach (var item in Alphabet)
                {
                    candidates.Add(Substitute(word, i, item));
                }

            }
            for (int i = 0; i < word.Length; i++)
            {
                candidates.Add(Delete(word, i));
            }
            return candidates.ToArray();
        }

        private static string Insert(string word, int pos, char c)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length+1];

            for (int i = 0 , j =0; i < newWord.Length;)
            {
                if(i == pos)
                {
                    newWord[i] = c;
                    i++;
                }
                else
                {
                    newWord[i] = wordChars[j];
                    i++;
                    j++;
                }
            }
            return new string(newWord);
        }

        private static string Delete(string word, int pos)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length-1];
            for (int i = 0, j = 0; i < newWord.Length;)
            {
                if (j == pos)
                {
                    
                    j++;
                }
                else
                {
                    newWord[i] = wordChars[j];
                    i++;
                    j++;
                }
            }
            return new string(newWord);
        }

        private static string Substitute(string word, int pos, char c)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length];
            for (int i = 0, j = 0; i < newWord.Length;)
            {
                if (i == pos)
                {
                    newWord[i] = c;
                    i++;
                    j++;
                }
                else
                {
                    newWord[i] = wordChars[j];
                    i++;
                    j++;
                }
            }
            return new string(newWord);
        }

    }
}
