using System;
using TestCommon;
using System.Linq;

namespace A9
{
    public class MergingTables : Processor
    {
        public MergingTables(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long[]>)Solve);

        public long[] Solve(long[] tableSizes, long[] sourceTables, long[] targetTables)
        {
            DisJointSet prob = new DisJointSet(tableSizes , sourceTables.Length);
            for (int i = 0; i < sourceTables.Length; i++)
            {
                prob.Merge(targetTables[i] - 1, sourceTables[i] - 1,i);
            }
            return prob.MaxInTime;
        }
    }
    class DisJointSet
    {
        public long[] Size { get; set; }

        public long[] Parents { get; set; }

        public long[] MaxInTime { get; set; }

        public long Count { get; set; }
        


        public DisJointSet(long[] sizes , long m)
        {
            Count = sizes.Length;
            Parents = new long[Count];
            for (int i = 0; i < Parents.Length; i++)
            {
                Parents[i] = i;
            }
            Size = new long[Count];
            for (int i = 0; i < Size.Length; i++)
            {
                Size[i] = sizes[i];
            }
            MaxInTime = new long[m];
        }

        public long FindParent(long i)
        {
            if (i != Parents[i])
            {
                return FindParent(Parents[i]);
            }
            return Parents[i];
        }

        public void Merge(long destination, long source , long i)
        {
            long destinationParent = FindParent(destination);
            long sourceParent = FindParent(source);

            if (destinationParent != sourceParent)
            {
                Parents[destinationParent] = sourceParent;
                Size[sourceParent] += Size[destinationParent];
            }
            this.MaxInTime[i] = Size.Max();

        }

    }
}