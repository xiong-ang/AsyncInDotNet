using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Others
{
    class SemaphoreDemo:IDemo
    {
        static SemaphoreSlim _sem = new SemaphoreSlim(3);    // 我们限制能同时访问的线程数量是3

        public void Demo()
        {
            for (int i = 1; i <= 50; i++)
            {
                object id = i;
                Task.Run(() => Enter(id));
            }
                
            Console.ReadLine();
        }    

        private void Enter(object id)
        {
            Console.WriteLine(id + " 开始排队...");
            _sem.Wait();
            Console.WriteLine(id + " 开始执行！");
            Thread.Sleep(10);
            Console.WriteLine(id + " 执行完毕，离开！");
            _sem.Release();
        }
    }
}
