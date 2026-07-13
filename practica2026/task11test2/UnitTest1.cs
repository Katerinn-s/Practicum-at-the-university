using task11gen;
using task11;
namespace task11test2
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
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
    }
}
