using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Clonable;
using Task6;

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
            //var var1 = new MyClass{a=15};
            //var var2 = var1.Clone();
            //Console.WriteLine(var2 + var2.GetHashCode().ToString() + var2.a);

            //var2.a = 9848;
            //Console.WriteLine(var1 + var1.GetHashCode().ToString() + var1.a);
            //Console.WriteLine(var2 + var2.GetHashCode().ToString() + var2.a);

            //Console.WriteLine();
            //Console.WriteLine();
            //var myClass = (MyClass)Class1.Create("MyClass");
            //Console.WriteLine(var1.a + "  " + var1.b);


            //Console.WriteLine();
            //Console.WriteLine();

            

            var integers = Task5.Class1.GetRandInteger();

            for (int i = 0; i < 8000; i++)
            {
                if(i==2000) Task5.Class1.SetRange(80);
                if(i==4000) Task5.Class1.SetRange(50000);
                if(i==6000) Task5.Class1.SetRange(50);
                Console.WriteLine(i + ". " + Task5.Class1.GetRandInteger());

            }


            Console.ReadKey();
        }
    }
}
