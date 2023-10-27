namespace StringCalculator.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StringCalculatorCreator creator = new StringCalculatorCreator();

            Console.WriteLine(creator.Add("//2[*3*5234*]12^+23|553"));
        }
    }
}