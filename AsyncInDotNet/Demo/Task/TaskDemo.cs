using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
    class TaskDemo:IDemo
    {
        public void Demo()
        {
            new Thread(Taskk).Start();  // .NET 1.0开始就有的
            Task.Factory.StartNew(Taskk); // .NET 4.0 引入了 TPL
            Task.Run(new Action(Taskk)); // .NET 4.5 新增了一个Run的方法

            /*
             * Task.Run(someAction);<==>
             * Task.Factory.StartNew(someAction, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
             */

            Console.WriteLine("我是主线程");
            Console.Read();
        }

        private void Taskk()
        {
            Console.WriteLine("我是另一个线程");
        }
    }
}
