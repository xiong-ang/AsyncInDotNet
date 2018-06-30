using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
    class ThreadDemo : IDemo
    {
        public void Demo()
        {
            Console.WriteLine("主线程开始！");

            //创建前台工作线程
            Thread t1 = new Thread(Task1);
            t1.Start();

            //创建后台工作线程
            Thread t2 = new Thread(new ParameterizedThreadStart(Task2));
            t2.IsBackground = true;//设置为后台线程
            t2.Start("传参");

            Console.Read();
        }

        private void Task1()
        {
            Thread.Sleep(10);
            Console.WriteLine("前台线程被调用！");
        }

        private void Task2(object data)
        {
            Thread.Sleep(20);
            Console.WriteLine("后台线程被调用！" + data);
        }
    }
}
