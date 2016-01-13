using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            OrderTest.GetOrders();
            //OrderTest.GetItemTransactions();

        }

    }
}
