using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class FastLM
    {
        public readonly WordCount[] WordCounts;


        public FastLM(WordCount[] wordCounts)
        {
            this.WordCounts = wordCounts.OrderBy(wc => wc.Word).ToArray();
        }

        public bool GetCount(string word, out ulong count)
        {
            count = 0;

            count = Binary(0, (ulong)WordCounts.Length - 1, word);
            

            if(count != 0)
            {
                return true; 
            }

            return false;
        }

        public ulong Binary(ulong left , ulong right , string  word)
        {
            if (right  < left)
                return 0;

            ulong mid = (left+right) / 2;

            var b = string.Compare(WordCounts[mid].Word, word);
            if (b < 0 )
            {
                return Binary(mid + 1, right , word);
            }
            else if(b > 0)
            {
                return Binary(left, mid-1, word);
            }
            else
            {
                return WordCounts[mid].Count;
            }

            
        }
       
    }
}
