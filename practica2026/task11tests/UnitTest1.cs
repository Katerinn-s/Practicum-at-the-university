//using task11;
using SystemCommands;
using Xunit;
using task11main;

namespace task11tests
{
    public class CalculatorTests
    {
       [Fact]
        public void Calculator_Add()
        {
            var generator = new CalculatorGenerator();
            string _calculatorCode =   "using System; " +
        "public class Calculator : task11.ICalculator " +
        "{ " +
        "    public int Add(int a, int b) => a + b; " +
        "    public int Minus(int a, int b) => a - b; " +
        "    public int Mul(int a, int b) => a * b; " +
        "    public int Div(int a, int b) => a / b; " +
        "}";
            dynamic calculator = generator.GenerateClass(_calculatorCode);

            int result = calculator.Add(5, 3);
            Assert.Equal(8, result);
        }
    }
}
