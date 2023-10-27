namespace PrimeFactor.UI
{
    public class PrimeFactorCreator
    {
        public string FindPrimes(double num)
        {
            if (num < 1 || num > 100)
                return "Invalid range. Please enter a number between 1-100.";
            else if (num == 1)
                return "1";

            string result = "1 Prime Prime ";
            bool isPrime = true;

            for (int i = 4; i <= num; i++)
            {
                for (int j = 2; j <= (i/2); j++)
                {
                    if (i % j == 0)
                    {
                        if (i % 2 == 0) 
                            result += i + " ";  
                        else 
                            result += "Composite ";

                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) result += "Prime ";
                isPrime = true;
            }

            return result;
        }
    }
}