using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Please enter a string:");
            string input = Console.ReadLine();

            GetFirstChar(input);
        }

        private static void GetFirstChar(string input)
        {
            try
            {
                Console.WriteLine("First char: " + input[0]);
            }
            catch(IndexOutOfRangeException ex) 
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured when trying to get the first element: " + ex);
            }
            finally 
            {
                Console.WriteLine("\nPlease enter a new string:");
                input = Console.ReadLine();
                GetFirstChar(input);
            }
        }
    }
}