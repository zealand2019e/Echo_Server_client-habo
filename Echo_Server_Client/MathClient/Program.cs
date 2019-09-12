using System;
using System.Threading.Tasks;

namespace MathClient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Type in a command ex. ADD 12 34. Available operations: ADD,SUB,MUL,DIV");
                string data = Console.ReadLine();
                new Client().Connect("localhost", data);
            }
        }
    }
}
