using TestCommon;
using System.Collections.Generic;
using System;

namespace A9
{
    public class ConvertIntoHeap : Processor
    {
        public ConvertIntoHeap(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(
            long[] array)
        {
            MinHeap prob = new MinHeap(array);
            for (int i = (int)prob.Count / 2; i >= 0; i--)
            {
                prob.SiftDown(i);
            }
            return prob.Swaps.ToArray();
        }
    }
    class MinHeap
    {
        public long[] Data { set; get; }
        public List<Tuple<long, long>> Swaps {set; get;}
        public long Count
        {
            get
            {
                return Data.Length;
            }
        }

        public MinHeap(long[] array)
        {
            this.Data = array;
            this.Swaps = new List<Tuple<long, long>>();
        }

        public void SiftDown(long i)
        {
            long indexOfMinChild = i;
            long left = LeftChild(i);
            if (left < this.Count  && Data[left] < Data[indexOfMinChild])
            {
                indexOfMinChild = left;
            }

            long right = RightChild(i);
            if(right < this.Count && Data[right] < Data[indexOfMinChild])
            {
                indexOfMinChild = right;
            }

            if(indexOfMinChild != i )
            {
                Swaps.Add(Tuple.Create(i, indexOfMinChild));
                long h =  Data[i];
                Data[i] = Data[indexOfMinChild];
                Data[indexOfMinChild] = h;
                SiftDown(indexOfMinChild);
            }
        }

        public long LeftChild(long i)
        {
            return (2 * i) + 1;
        }
        public long RightChild(long i)
        {
            return (2 * i) + 2;
        }
    }

}