using System;

namespace Echo_Client
{
    class Program
    {
        static void Main(string[] args)
        {
             new Client().Connect("localhost","message word word word");
        }
    }
}
