using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clonable;

namespace CloneTest
{
    [Serializable]
    public class MyClass
    {
        public int a = 5;
        public string b = "sdfsdfs";

    }
    class Program
    {
        static void Main(string[] args)
        {
            var var1 = new MyClass{a=15};
            var var2 = var1.Clone();
            Console.WriteLine(var2 + var2.GetHashCode().ToString() + var2.a);

            var2.a = 9848;
            Console.WriteLine(var1 + var1.GetHashCode().ToString() + var1.a);
            Console.WriteLine(var2 + var2.GetHashCode().ToString() + var2.a);

            Console.ReadKey();
        }
    }
}
