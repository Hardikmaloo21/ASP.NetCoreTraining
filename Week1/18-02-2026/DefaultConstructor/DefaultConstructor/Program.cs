using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
namespace constr
{

    class Constanddest
    {
        private static int number;
        public static int Number
        {
            get
            {
                return number;
            }
        }

        static Constanddest()
        {
            Random r = new Random();
            number = r.Next();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("static number = " + Constanddest.Number);
        }
    }
}