namespace StringCalculator.UI
{
    public class StringCalculatorCreator
    {
        public int Add(string numbers)
        {
            if (numbers == string.Empty) return 0;

            int finalSum = 0;
            int counter = 0;
            if (numbers.Any(x => (int)x > 47 && (int)x < 58))
            {
                string numsToAdd = String.Empty;
                bool isStillNum = false;

                while (counter < numbers.Length)
                {
                    if ((int)numbers[counter] > 47 && (int)numbers[counter] < 58)
                    {
                        numsToAdd += numbers[counter].ToString();
                        isStillNum = true;
                    }
                    else isStillNum = false;
                    
                    if ((!isStillNum && !String.IsNullOrEmpty(numsToAdd)) && (Convert.ToInt32(numsToAdd) <= 1000) || (counter == numbers.Length - 1))
                    {
                        finalSum += Convert.ToInt32(numsToAdd);
                        numsToAdd = String.Empty;
                    }

                    if (!String.IsNullOrEmpty(numsToAdd) && Convert.ToInt32(numsToAdd) > 1000) numsToAdd = String.Empty;

                    counter++;
                }   
            }

            return finalSum;
        }
    }
}