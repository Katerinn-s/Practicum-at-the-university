//using task11;
using SystemCommands;
using Xunit;
using task11main;

namespace task11tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Calculator_Add_ReturnsCorrectSum()
        {
            var generator = new CalculatorGenerator();
            dynamic calculator = generator.CreateCalculatorInstance();

            int result = calculator.Add(5, 3);
            Assert.Equal(8, result);
        }
    }
}
