namespace PrimeFactor.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrimeFactorCreator creator = new PrimeFactorCreator();

            Console.WriteLine(creator.FindPrimes(100));
        }
    }
}