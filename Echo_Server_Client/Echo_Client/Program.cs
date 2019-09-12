using System;
using System.Threading.Tasks;

namespace Echo_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Press a key to send the message");
                Console.ReadKey();
                Task.Run(()=> new Client().Connect("localhost", "message word word word"));
            }
             
        }
    }
}
