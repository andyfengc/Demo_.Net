using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace PWSTestClient
{
    class Util
    {
        public static void Print(string name, object o1, object o2)
        {
            Console.Write(offset);
            Console.WriteLine(name + ": {0} {1}", o1, o2);
        }

        public static void Print(string name, object o)
        {
            Console.Write(offset);
            if (o is string && string.IsNullOrEmpty((string)o))
                Console.WriteLine(name + ": - nil");
            else
                Console.WriteLine(name + ": {0}", o);
        }

        public static void Print(string name)
        {
            Console.Write(offset);
            Console.WriteLine(name);
        }

        private static string offset = string.Empty;
        public static void Push()
        {
            offset += "\t";
        }
        public static void Pop()
        {
            if (offset.Length > 0)
                offset = offset.Remove(offset.Length - 1, 1);
        }
    }
}