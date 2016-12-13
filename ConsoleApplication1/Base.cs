using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Base
    {

        public void Print()
        {
#if PI1
            Console.WriteLine("PI1");
#endif
#if PI2
            Console.WriteLine("PI2");
#endif

        }
    }
}
