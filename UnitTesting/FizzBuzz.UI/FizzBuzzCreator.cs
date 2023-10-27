namespace FizzBuzz.UI
{
    public class FizzBuzzCreator
    {
        public string FindFizzOrBuzz(int range)
        {
            if (range >= 1 && range <= 100)
            {
                List<string> values = new List<string>();
                string result = String.Empty;

                for (int i = 1; i <= range; i++)
                {
                    if (i % 3 == 0 && i % 5 == 0)
                        values.Add("FizzBuzz ");
                    else if (i % 3 == 0)
                        values.Add("Fizz ");
                    else if (i % 5 == 0)
                        values.Add("Buzz ");
                    else
                        values.Add(i.ToString() + " ");
                }

                values.ForEach(value => result += value);

                return result.TrimEnd();
            }

            return "Out of range! Please enter a value between 1-100..";
        }
    }
}