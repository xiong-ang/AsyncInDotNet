using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
    class ThreadPoolDemo:IDemo
    {
        public void Demo()
        {
            //https://blog.csdn.net/zhaoguanghui2012/article/details/52910035
            Console.WriteLine("主线程开始！");

            //工作者线程最大数目，I/O线程的最大数目
            ThreadPool.SetMaxThreads(1000, 1000); 

            //创建要执行的任务
            WaitCallback workItem = new WaitCallback(Task); 

            //重复调用10次
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(workItem);
            }

            //程序并没有每次执行任务都创建新的线程，而是循环利用线程池中维护的线程
            Console.Read();
        }

        private void Task(object state)
        {
            Console.WriteLine("当前线程Id为：" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
