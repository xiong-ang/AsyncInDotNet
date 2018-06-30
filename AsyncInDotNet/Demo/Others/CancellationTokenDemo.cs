using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Others
{
    class CancellationTokenDemo:IDemo
    {
        //https://msdn.microsoft.com/en-us/library/system.threading.cancellationtokensource(v=vs.110).aspx
        /*
         The general pattern for implementing the cooperative cancellation model is:

        1. Instantiate a CancellationTokenSource object, which manages and sends cancellation notification to the individual cancellation tokens.

        2. Pass the token returned by the CancellationTokenSource.Token property to each task or thread that listens for cancellation.

        3. Call the CancellationToken.IsCancellationRequested method from operations that receive the cancellation token. Provide a mechanism for each task or thread to respond to a cancellation request. Whether you choose to cancel an operation, and exactly how you do it, depends on your application logic.

        4. Call the CancellationTokenSource.Cancel method to provide notification of cancellation. This sets the CancellationToken.IsCancellationRequested property on every copy of the cancellation token to true.

        5. Call the Dispose method when you are finished with the CancellationTokenSource object.
         */

        public void Demo()
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Random rnd = new Random();
            Object lockObj = new Object();

            List<Task<int[]>> tasks = new List<Task<int[]>>();
            TaskFactory factory = new TaskFactory(token);
            for (int taskCtr = 0; taskCtr <= 10; taskCtr++)
            {
                int iteration = taskCtr + 1;
                tasks.Add(factory.StartNew(() =>
                {
                    int value;
                    int[] values = new int[10];
                    for (int ctr = 1; ctr <= 10; ctr++)
                    {
                        lock (lockObj)
                        {
                            value = rnd.Next(0, 101);
                        }
                        if (value == 0)
                        {
                            source.Cancel();
                            Console.WriteLine("Cancelling at task {0}", iteration);
                            break;
                        }
                        values[ctr - 1] = value;
                    }
                    return values;
                }, token));

            }
            try
            {
                Task<double> fTask = factory.ContinueWhenAll(tasks.ToArray(),
                                                             (results) =>
                                                             {
                                                                 Console.WriteLine("Calculating overall mean...");
                                                                 long sum = 0;
                                                                 int n = 0;
                                                                 foreach (var t in results)
                                                                 {
                                                                     foreach (var r in t.Result)
                                                                     {
                                                                         sum += r;
                                                                         n++;
                                                                     }
                                                                 }
                                                                 return sum / (double)n;
                                                             }, token);
                Console.WriteLine("The mean is {0}.", fTask.Result);
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                        Console.WriteLine("Unable to compute mean: {0}",
                                          ((TaskCanceledException)e).Message);
                    else
                        Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
            finally
            {
                source.Dispose();
            }
        }
    }
}
