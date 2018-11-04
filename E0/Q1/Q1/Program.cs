using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class Program
    {
        private static Dictionary<int, char[]> D =
            new Dictionary<int, char[]>
            {
                [0] = new char[] { '+' },
                [1] = new char[] { '_', ',', '@' },
                [2] = new char[] { 'A', 'B', 'C' },
                [3] = new char[] { 'D', 'E', 'F' },
                [4] = new char[] { 'G', 'H', 'I' },
                [5] = new char[] { 'J', 'K', 'L' },
                [6] = new char[] { 'M', 'N', 'O' },
                [7] = new char[] { 'P', 'Q', 'R', 'S' },
                [8] = new char[] { 'T', 'U', 'V' },
                [9] = new char[] { 'W', 'X', 'Y', 'Z' },
            };


        public static string[] GetNames(int[] phone)
        {
            if(phone.Length == 1)
            {
                List<string> a = new List<string>();
                foreach (var item in D[phone[0]])
                {
                    a.Add(item.ToString());
                }
                return a.ToArray();
            }
            if(phone.Length==2)
            {
                List<string> a = new List<string>();

                foreach (var item1 in D[phone[0]])
                {
                    foreach (var item2 in D[phone[1]])
                    {
                        a.Add(item1.ToString() + item2.ToString());
                    }
                }

                return a.ToArray();
            }

            string[] left  = GetNames(phone.Take(phone.Length/2).ToArray());
            string[] right = GetNames(phone.Skip(phone.Length/2).ToArray());
            List<string> b = new List<string>();
            foreach (var item1 in left)
            {
                foreach (var item2 in right)
                {
                    b.Add(item1 + item2);
                }
            }

            return b.ToArray();
            
        }

        

        static void Main(string[] args)
        {
            int[] phoneNumber1 = new int[] { 0, 9, 1, 2 };
            int[] phoneNumber2 = new int[] { 0, 9, 1, 2, 2};
            int[] phoneNumber3 = new int[] { 0, 9, 1, 2, 2, 2, 4, 2, 5, 2, 5 };
            int[] phoneNumber4 = new int[] { 0, 9, 1, 2, 2, 2, 4, 2, 5, 2, 5 };

            GetNames(phoneNumber1);

            //// چاپ یک رشته حرفی برای شماره تلفن
            //for (int i = 0; i < phoneNumber.Length; i++)
            //    Console.Write(D[phoneNumber[i]][0]);
            //Console.WriteLine();
        }
    }
        
}
