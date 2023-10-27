using PrimeFactor.UI;

namespace PrimeFactor.Tests
{
    public class PrimeFactorTests
    {
        [Fact]
        public void Return_InvalidRangeError_IfParameterIsNotInRange()
        {
            var expectedResult = "Invalid range. Please enter a number between 1-100.";

            PrimeFactorCreator pfc = new PrimeFactorCreator();
            var actualResult = pfc.FindPrimes(101);
            //var actualResult = pfc.FindPrimes(0);
            //var actualResult = pfc.FindPrimes(-1);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Return_PrimesAndComposites_IfParametersInRange()
        {
            //var expectedResult = "1 Prime Prime 4 Prime 6 Prime 8 Composite 10 Prime 12 Prime 14 Composite 16 Prime 18 Prime 20 Composite 22 Prime 24 Composite 26 Composite 28 Prime 30 Prime 32 Composite 34 Composite 36 Prime 38 Composite 40 Prime 42 Prime 44 Composite 46 Prime 48 Composite 50 Composite 52 Prime 54 Composite 56 Composite 58 Prime 60 Prime 62 Composite 64 Composite 66 Prime 68 Composite 70 Prime 72 Prime 74 Composite 76 Composite 78 Prime 80 Composite 82 Prime 84 Composite 86 Composite 88 Prime 90 Composite 92 Composite 94 Composite 96 Prime 98 Composite 100 ";
            //var expectedResult = "1 Prime Prime 4 Prime ";
            var expectedResult = "1 Prime Prime 4 Prime 6 Prime 8 Composite 10 Prime 12 Prime 14 Composite 16 Prime 18 Prime 20 Composite 22 Prime 24 Composite 26 Composite 28 Prime 30 Prime 32 Composite 34 Composite 36 Prime 38 Composite 40 Prime 42 Prime 44 Composite 46 Prime 48 Composite 50 Composite 52 Prime ";

            PrimeFactorCreator pfc = new PrimeFactorCreator();
            //var actualResult = pfc.FindPrimes(100);
            //var actualResult = pfc.FindPrimes(5);
            var actualResult = pfc.FindPrimes(53);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}