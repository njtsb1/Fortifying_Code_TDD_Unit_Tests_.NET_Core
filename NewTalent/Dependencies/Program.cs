using System;
using NewTalent.Dependencies;

namespace NewTalent.Dependencies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== TDD Calculator Initialized ===");
            
            Calculator calc = new Calculator();
            
            // Basic execution sample
            int sumResult = calc.Add(10, 5);
            Console.WriteLine($"Sample Execution: 10 + 5 = {sumResult}");
        }
    }
}
