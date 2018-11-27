using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class PacketProcessing : Processor
    {
        public PacketProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long[]>)Solve);

        public long[] Solve(long bufferSize, 
            long[] arrivalTimes, 
            long[] processingTimes)
        {

            long[] startingProcessingTime = new long[arrivalTimes.Length];



            List<long> buffer = new List<long>() { };

            for (int i = 0; i < arrivalTimes.Length; i++)
            {


                if (buffer.Count < bufferSize)
                {


                    if (buffer.Count == 0)
                    {
                        buffer.Add(arrivalTimes[i] + processingTimes[i]);
                        startingProcessingTime[i] = arrivalTimes[0];
                        continue;
                    }


                    else
                    {
                        if (buffer[buffer.Count - 1] > arrivalTimes[i])
                        {
                            startingProcessingTime[i] = buffer[buffer.Count - 1] > arrivalTimes[i] ? buffer[buffer.Count - 1] : arrivalTimes[i];
                            buffer.Add(buffer[buffer.Count - 1] + processingTimes[i]);
                        }
                        else
                        {
                            startingProcessingTime[i] = buffer[buffer.Count - 1] > arrivalTimes[i] ? buffer[buffer.Count - 1] : arrivalTimes[i];
                            buffer.Add(arrivalTimes[i] + processingTimes[i]);

                        }

                    }

                }
                else
                {
                    if (buffer[0] > arrivalTimes[i])
                    {
                        startingProcessingTime[i] = -1;
                        continue;
                    }
                    else
                    {
                        buffer.RemoveAt(0);
                        if (buffer.Count == 0)
                        {
                            startingProcessingTime[i] = arrivalTimes[i];
                            buffer.Add(arrivalTimes[i] + processingTimes[i]);
                            continue;
                        }


                        if (buffer[buffer.Count - 1] > arrivalTimes[i])
                        {
                            startingProcessingTime[i] = buffer[buffer.Count - 1] > arrivalTimes[i] ? buffer[buffer.Count - 1] : arrivalTimes[i];


                            buffer.Add(buffer[buffer.Count - 1] + processingTimes[i]);
                        }


                        else
                        {
                            startingProcessingTime[i] = buffer[buffer.Count - 1] > arrivalTimes[i] ? buffer[buffer.Count - 1] : arrivalTimes[i];
                            buffer.Add(arrivalTimes[i] + processingTimes[i]);
                        }
                    }
                }
            }

            return startingProcessingTime.ToArray();
        }
    }
   
}
