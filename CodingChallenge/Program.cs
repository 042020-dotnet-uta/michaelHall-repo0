using System;

namespace CodingChallenge
{
    class Program
    {
    
        static void Main(string[] args)
        {
            // first initialize the variables for counting each scenario
            int sweetCount = 0;
            int saltyCount = 0;
            int sweetSaltyCount = 0;

            // create a for loop to iterate from 1 - 100
            for (int num = 1; num < 101; num++)
            {
               // first check if the number is divisible by 3 & 5
               if ((num % 3) == 0 && (num % 5) == 0)
                {
                    Console.WriteLine("sweet'nSalty");
                    sweetSaltyCount++; // add one to the multiple of 3 & 5 secanario
                }
               else if (num % 3 == 0) // then check if divisible by 3
                {
                    Console.WriteLine("sweet");
                    sweetCount++; // add one to the multiple of 3 scenario
                }
               else if (num % 5 == 0) // then check if divisible by 5
                {
                    Console.WriteLine("salty");
                    saltyCount++; // add one to the multiple of 5 scenario
                }
               else
                {
                    Console.WriteLine(num); // prints out the number if none of the scenarios happened
                }
            }

            // finally print out the total number of each scenario
            Console.WriteLine($"There were {sweetCount} sweet's, {saltyCount} salty's," +
                $" and {sweetSaltyCount} sweet'nSalty's!");

        }
    }
}
