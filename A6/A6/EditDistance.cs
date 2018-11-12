using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class EditDistance: Processor
    {
        public EditDistance(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long>)Solve);

        public long Solve(string str1, string str2)
        {

            long[,] editDistance = new long[str1.Length+1, str2.Length+1];
            for (int i = 0; i <= str1.Length; i++)
            {
                editDistance[i, 0] = i; 
            }
            for (int i = 0; i <= str2.Length; i++)
            {
                editDistance[0, i] = i;
            }

            long insert, delete, match, substitute;

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    insert = editDistance[i, j-1] + 1;
                    delete = editDistance[i-1 , j] + 1;
                    substitute = editDistance[i - 1, j - 1]+1;
                    match = editDistance[i - 1, j - 1];

                    if (str1[i-1] == str2[j-1])
                    {
                        editDistance[i, j] = Math.Min(insert, Math.Min(delete, match));
                    }
                    else
                    {
                        editDistance[i , j] = Math.Min(insert, Math.Min(delete, substitute));
                    }
                }
            }
            return editDistance[str1.Length , str2.Length];
        }

    }
}
