
using Xunit;
using task14;

namespace task14tests
{
    public class UnitTest1
    {
        [Fact]
        public void IntegralXFromMinus1To1Returns0()
        {
            var X = (double x) => x;
            double result = DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2);
            Assert.Equal(0, result, 4);
        }

        [Fact]
        public void IntegralSinFromMinus1To1Returns0()
        {
            var SIN = (double x) => Math.Sin(x);
            double result = DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8);
            Assert.Equal(0, result, 4);
        }

        [Fact]
        public void IntegralXFrom0To5Returns12point5()
        {
            var X = (double x) => x;
            double result = DefiniteIntegral.Solve(0, 5, X, 1e-6, 8);
            Assert.Equal(12.5, result, 4);
        }
    }
}
