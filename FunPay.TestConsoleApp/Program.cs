using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FunPay.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
           Core.Core core = new Core.Core();
           
            core.Start();
            core.IsScreen = true;

            Console.WriteLine("Start");
            Console.ReadLine();
        }
    }
}
