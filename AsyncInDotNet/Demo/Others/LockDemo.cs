using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Others
{
    class LockDemo:IDemo
    {
        private static bool _isDone = false;
        private static object _lock = new object();

        public void Demo()
        {
            Task.Run(new Action(Done));
            Task.Run(new Action(Done));
            Console.ReadLine();
        }

        private void Done()
        {
            lock (_lock)
            {
                if (!_isDone)
                {
                    Console.WriteLine("Done"); // 只能执行一次
                    _isDone = true;
                }
            }
        }

        
    }
}
