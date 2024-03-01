using System;
using Lab9_10CharpT;

class Program
{
    static void Main()
    {
        Console.WriteLine("Lab 10 CSharp");

        while (true)
        {
            Console.WriteLine("=========================================================");
            Console.WriteLine("Select a task:");
            Console.WriteLine("1. Task 1");
            Console.WriteLine("2. Task 2");
            Console.WriteLine("3. Exit");
            Console.WriteLine("=========================================================");
            Console.Write("Enter your choice >>> ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Task1.Task1_();
                    break;

                /* case "2":
                     Task2.Task2_();
                     break;*/

                case "3":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}
