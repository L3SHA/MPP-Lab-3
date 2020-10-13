using System;

namespace ExtensionMethodTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Useful useful = new Useful();
            Console.WriteLine(useful.GetExtension(10));
            
        }
    }

    public class Useful
    {
        public void SomeUsefulMethod()
        {

        }
    }

    public static class Extension
    {
        public static int GetExtension(this Useful useful, int num)
        {
            return num;
        }
    }
}
