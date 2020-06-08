using System;

namespace CoreBoy
{
    class Program
    {
        static void Main(string[] args)
        {
            CPU cpu = new CPU();
            cpu.Loop();
            Console.Read();
        }
    }
}
