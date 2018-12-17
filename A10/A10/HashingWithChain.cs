using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A10
{
    public class HashingWithChain : Processor
    {
        public HashingWithChain(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, string[], string[]>)Solve);

        protected List<Tuple<string, long>> HashTable;
        public long BucketCount { get; set; }

        public string[] Solve(long bucketCount, string[] commands)
        {
            List<string> result = new List<string>();
            HashTable = new List<Tuple<string, long>>();
            BucketCount = bucketCount;
            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var arg = toks[1];

                switch (cmdType)
                {
                    case "add":
                        Add(arg);
                        break;
                    case "del":
                        Delete(arg);
                        break;
                    case "find":
                        result.Add(Find(arg));
                        break;
                    case "check":
                        result.Add(Check(int.Parse(arg)));
                        break;
                }
            }
            return result.ToArray();
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;

        public static long PolyHash(
            string str, int start, int count,
            long p = BigPrimeNumber, long x = ChosenX)
        {
            long hash = 0;

            for (int i = str.Length - 1; i >= 0 ; i--)
            {
                hash = ((hash * ChosenX + str[i]) % BigPrimeNumber + BigPrimeNumber) % BigPrimeNumber;
            }
            
            return hash % count;
        }

        public void Add(string str)
        {
            if(!HashTable.Select(x => x.Item1).Contains(str))
            {
                HashTable.Add(Tuple.Create(str, PolyHash(str, 0, (int)BucketCount)));
            }
        }

        public string Find(string str)
        {
            if(HashTable.Select(x => x.Item1).Contains(str))
            {
                return "yes";
            }
            return "no";
        }

        public void Delete(string str)
        {
            if(HashTable.Select(x => x.Item1).Contains(str))
            {
                HashTable.Remove(HashTable.Where(x => x.Item1 == str).First());
            }
        }

        public string Check(int i)
        {
            var temp = HashTable.Where(x => x.Item2 == i);
            if (HashTable.Count != 0 && temp.Count() != 0)
            {
                return string.Join(" ", temp.Select(x => x.Item1).Reverse());
            }

            return "-";
        }
    }
}
