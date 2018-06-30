using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
    class ParallelDemo:IDemo
    {
        public void Demo()
        {
            //数据并行:是指使用Parallel.For()或Parallel.ForEach()方法以并行方式对数组或集合中的数据进行迭代
            ParallelLoopResult result = Parallel.For(0, 10000, i =>
            {
                Console.WriteLine("{0}, task: {1} , thread: {2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            });
        }
    }
}
