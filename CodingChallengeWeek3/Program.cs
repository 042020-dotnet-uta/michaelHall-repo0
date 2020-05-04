using Microsoft.VisualBasic;
using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallengeWeek3
{
    class Program
    {
        static void Main(string[] args)
        {
            // declares input variable
            string input = "";
            do
            {
                // prints out the instruction menu
                MenuPrompt();
                do
                {
                    // gets input from the user
                    input = Console.ReadLine();
                } while (!IsValidInput(input)); // checks if input is valid
            } while (input != "4"); // if fourth option, exit the program
        }

        static void MenuPrompt()
        {
            // prints menu instructions
            Console.WriteLine("Welcome to the Menu! Type the respective number to do one of the following tasks");
            Console.WriteLine("1: Is the number even?");
            Console.WriteLine("2: Multiplication Table");
            Console.WriteLine("3: Alternating Elements");
            Console.WriteLine("4: Exit the application");
        }
        static bool IsValidInput(string input)
        {
            // switch case to determine which option
            switch(input)
            {
                case "1": // is the number even task
                    IsEven();
                    break;
                case "2": // multiplication table task
                    MultTable();
                    break;
                case "3": // alternating elements task
                    Shuffle();
                    break;
                case "4": // exits
                    break;
                default: // try again if option was not picked
                    Console.WriteLine("Invalid option, enter one of the 4.");
                    return false;
            }
            return true;
        }

        static void IsEven()
        {
            // asks for int and sets it to variable number
            Console.WriteLine("Enter a number (int) and see if it's even.");
            var number = Console.ReadLine();
            int anInt;
            if (!int.TryParse(number, out anInt)) // checks if it's not an int
            {
                Console.WriteLine($"{number} is a {number.GetType()}, not an int");
            }
            else
            {
                if (anInt % 2 == 0) // checks if even
                {
                    Console.WriteLine($"{anInt} is an even number!");
                }
                else // if odd
                {
                    Console.WriteLine($"{anInt} is not an even number, it's odd");
                }
            }
        }

        static void MultTable()
        {
            // asks for int and sets input to number
            Console.WriteLine("Enter a number (int) to see the multiplication table");
            var number = Console.ReadLine();
            int anInt;
            if (!int.TryParse(number, out anInt)) // checks if int
            {
                Console.WriteLine($"{number} is a {number.GetType()}, not an int");
            }
            else
            {
                // nested for loop to print out table
                for(int i = 1; i <= anInt; i++) // from 1 to number
                {
                    for (int j = 1; j <= anInt; j++)
                    {
                        // prints out "i x j = product of i and j"
                        Console.WriteLine($"{i} x {j} = {i*j}");
                    }
                }
            }
        }

        static void Shuffle()
        {
            Console.WriteLine("Enter 5 things individually to put into list 1");
            // creates a list of 5 objects
            List<object> firstList = new List<object>();
            for (int i = 0; i < 5; i++)
            {
                var input = Console.ReadLine();
                while (input == "")
                {
                    input = Console.ReadLine();
                }
                firstList.Add(input); // objects stored if not null
            }

            Console.WriteLine("Enter 5 things individually to put into list 2 now");
            // creates a second list of 5 objects
            List<object> secondList = new List<object>();
            for (int i = 0; i < 5; i++)
            {
                var input = Console.ReadLine();
                while (input == "")
                {
                    input = Console.ReadLine();
                }
                secondList.Add(input);
            }

            List<object> combinedList = new List<object>();
            // combines the two lists alternating through the indeces
            for (int i = 0; i < 5; i++)
            {
                combinedList.Add(firstList[i]);
                combinedList.Add(secondList[i]);
            }

            // prints out the list in the format [0, 1, 2, etc...]
            Console.WriteLine("Here's the combined list:");
            Console.Write("[");
            foreach (var element in combinedList)
            {
                if (combinedList[0] != element)
                {
                    Console.Write($", ");
                }
                Console.Write($"{element}");
            }
            Console.WriteLine("]");
        }
    }
}
