using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class PLINQDemo:IDemo
    {
        public void Demo()
        {
            List<int> source = new List<int>();

            //并行LINQ查询
            int[] modThreeIsZero = (from num in source.AsParallel()
                                    where num % 3 == 0
                                    orderby num descending
                                    select num).ToArray();
        }
    }
}
