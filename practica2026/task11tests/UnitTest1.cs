using task11;
using task11gen;

namespace task11tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Calculator_Add_ReturnsCorrectSum()
        {
            string _calculatorCode =
       "using System; " +
       "public class Calculator : task11.ICalculator " +
       "{ " +
       "    public int Add(int a, int b) => a + b; " +
       "    public int Minus(int a, int b) => a - b; " +
       "    public int Mul(int a, int b) => a * b; " +
       "    public int Div(int a, int b) => a / b; " +
       "}";
            var generator = new CalculatorGenerator();
            ICalculator calculator = generator.CreateCalculatorInstance(_calculatorCode);
            int result = calculator.Add(5, 3);
            Assert.Equal(8, result);
        }

        [Fact]
        public void Calculator_Minus_ReturnsCorrectDifference()
        {
            string _calculatorCode =
                "using System; " +
                "public class Calculator : task11.ICalculator " +
                "{ " +
                "    public int Add(int a, int b) => a + b; " +
                "    public int Minus(int a, int b) => a - b; " +
                "    public int Mul(int a, int b) => a * b; " +
                "    public int Div(int a, int b) => a / b; " +
                "}";
            var generator = new CalculatorGenerator();
            ICalculator calculator = generator.CreateCalculatorInstance(_calculatorCode);
            int result = calculator.Minus(10, 4);
            Assert.Equal(6, result);
        }

        [Fact]
        public void Calculator_Mul_ReturnsCorrectProduct()
        {
            string _calculatorCode =
                "using System; " +
                "public class Calculator : task11.ICalculator " +
                "{ " +
                "    public int Add(int a, int b) => a + b; " +
                "    public int Minus(int a, int b) => a - b; " +
                "    public int Mul(int a, int b) => a * b; " +
                "    public int Div(int a, int b) => a / b; " +
                "}";
            var generator = new CalculatorGenerator();
            ICalculator calculator = generator.CreateCalculatorInstance(_calculatorCode);
            int result = calculator.Mul(6, 7);
            Assert.Equal(42, result);
        }
    }
}
