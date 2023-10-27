using StringCalculator.UI;

namespace StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        StringCalculatorCreator stringCalculator = new StringCalculatorCreator();

        [Fact]
        public void Return_Zero_IfInputIsEmpty()
        {
            int expectedResult = 0;

            int actualResult = stringCalculator.Add(String.Empty);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Return_One_IfInputIsOne()
        {
            int expectedResult = 1;

            int actualResult = stringCalculator.Add("1");

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Return_Itself_IfInputIsSingle()
        {
            int expectedResult = 35;

            int actualResult = stringCalculator.Add("35");

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Return_SumOfAllNumbers()
        {
            int expectedResult = 73;

            int actualResult = stringCalculator.Add("10,25,38");

            Assert.Equal(expectedResult, actualResult);
        }
    }
}