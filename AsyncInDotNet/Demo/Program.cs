using Demo.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Demo(new ThreadDemo());
            //Demo(new ThreadPoolDemo());

            //Demo(new ParallelDemo());
            //Demo(new PLINQDemo());
            //Demo(new TaskDemo());
            //Demo(new AsyncAwaitDemo());

            //Demo(new LockDemo());
            //Demo(new SemaphoreDemo());
            //Demo(new CancellationTokenDemo());
            Demo(new TaskSchedulerDemo());
        }

        private static void Demo(IDemo exampleDemo)
        {
            exampleDemo.Demo();
        }
    }
}
