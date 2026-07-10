using task11;

namespace task11tests
{
    public class CalculatorTest
    {
        [Fact]
        public void Calculator_Add_ReturnsCorrectSum()
        {
            var generator = new CalculatorGenerator();
            dynamic calculator = generator.CreateCalculatorInstance();

            int result = calculator.Add(5, 3);
            Assert.Equal(8, result);
        }

        [Fact]
        public void Calculator_Minus_ReturnsCorrectDifference()
        {
            var generator = new CalculatorGenerator();
            dynamic calculator = generator.CreateCalculatorInstance();

            int result = calculator.Minus(10, 4);
            Assert.Equal(6, result);
        }

        [Fact]
        public void Calculator_Mul_ReturnsCorrectProduct()
        {
            var generator = new CalculatorGenerator();
            dynamic calculator = generator.CreateCalculatorInstance();

            int result = calculator.Mul(6, 7);
            Assert.Equal(42, result);
        }

        [Fact]
        public void Calculator_Div_ReturnsCorrectQuotient()
        {
            var generator = new CalculatorGenerator();
            dynamic calculator = generator.CreateCalculatorInstance();

            int result = calculator.Div(15, 3);
            Assert.Equal(5, result);
        }

        [Fact]
        public void Calculator_Div_ByZero_ThrowsDivideByZeroException()
        {
            var generator = new CalculatorGenerator();
            dynamic calculator = generator.CreateCalculatorInstance();

            Assert.Throws<System.DivideByZeroException>(() => calculator.Div(10, 0));
        }

        [Fact]
        public void Calculator_UsingInterface_WorksCorrectly()
        {
            var generator = new CalculatorGenerator();
            dynamic calculator = generator.CreateCalculatorInstance();
            ICalculator calcWrapper = new CalculatorWrapper(calculator);

            Assert.Equal(30, calcWrapper.Add(10, 20));
            Assert.Equal(10, calcWrapper.Minus(20, 10));
            Assert.Equal(25, calcWrapper.Mul(5, 5));
            Assert.Equal(5, calcWrapper.Div(20, 4));
        }
    }
}
