using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task11;
using System.IO;

namespace SystemCommands
{
    public class CalculatorWrapper : ICalculator
    {
        private readonly dynamic _calculator;

        public CalculatorWrapper(dynamic calculator)
        {
            _calculator = calculator;
        }

        public int Add(int a, int b) => _calculator.Add(a, b);
        public int Minus(int a, int b) => _calculator.Minus(a, b);
        public int Mul(int a, int b) => _calculator.Mul(a, b);
        public int Div(int a, int b) => _calculator.Div(a, b);
    }
}
