namespace FizzBuzz.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FizzBuzzCreator fizzBuzz = new FizzBuzzCreator();

            Console.WriteLine(fizzBuzz.FindFizzOrBuzz(15));
        }
    }
}