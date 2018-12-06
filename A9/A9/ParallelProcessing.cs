using System;
using System.Collections.Generic;
using TestCommon;

namespace A9
{
    public class ParallelProcessing : Processor
    {
        public ParallelProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(long threadCount, long[] jobDuration)
        {
            Tuple<long, long>[] Priority = new Tuple<long, long>[jobDuration.Length];
            ThreadsMinHeap minHeap = new ThreadsMinHeap(threadCount);
            for (int i = 0; i < jobDuration.Length; i++)
            {
                Priority[i] = Tuple.Create(minHeap.Threads[0].Number, minHeap.Threads[0].StartTime);
                minHeap.ChangePriority(0, minHeap.Threads[0].StartTime + jobDuration[i]);
            }
            return Priority;
        }
    }

    class ThreadsMinHeap
    {
        public long Count;
        public Thread[] Threads { get; set; }
        public ThreadsMinHeap(long threadCount)
        {
            this.Count = threadCount;
            Threads = new Thread[Count];
            for (int i = 0; i < Count; i++)
            {
                Threads[i] = new Thread(i, 0);
            }
        }

        public void ChangePriority(long index, long priority)
        {
            long lastPriority = this.Threads[index].StartTime;
            this.Threads[index] = new Thread(Threads[index].Number, priority);
            if(priority< lastPriority)
            {
                SiftUp(index);
            }
            else
            {
                SiftDown(index);
            }
            SiftDown(index);
        }

        private void SiftDown(long i)
        {
            long indexOfMinChild = i;
            long left = LeftChild(i);
            if (left < this.Count && this.Compare(Threads[left] , Threads[indexOfMinChild]))
            {
                indexOfMinChild = left;
            }

            long right = RightChild(i);
            if (right < this.Count && this.Compare(Threads[right] , Threads[indexOfMinChild]))
            {
                indexOfMinChild = right;
            }

            if (indexOfMinChild != i)
            {
                var h = Threads[i];
                Threads[i] = Threads[indexOfMinChild];
                Threads[indexOfMinChild] = h;
                SiftDown(indexOfMinChild);
            }
        }

        private void SiftUp(long i)
        {
            while (i > 0 && Compare(Threads[i], Threads[Parent(i)]))
            {
                var h = Threads[i];
                Threads[i] = Threads[Parent(i)];
                Threads[Parent(i)] = h;
                i = Parent(i);
            }
        }

        private long Parent(long i) => Threads[(i - 1) / 2].Number;
        private long LeftChild(long i) => (2 * i) + 1;
        private long RightChild(long i) => (2 * i) + 2;

        public bool Compare(Thread a , Thread b)
        {
            if (a.StartTime != b.StartTime)
            {
                return a.StartTime < b.StartTime;
            }
            else
            {
                return a.Number < b.Number;
            }
        }
    }
    class Thread
    {
        public long Number { set; get; }
        public long StartTime { set; get; }
        public Thread (long num , long startTime)
        {
            this.Number = num;
            this.StartTime = startTime;
        }

    }

}
