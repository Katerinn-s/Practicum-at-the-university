using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using task11;
using task11gen;

namespace task11Console
{
    internal class Program
    {
        static void Main(string[] args)
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
            int result2 = calculator.Add(5, 3);
            Console.WriteLine(result2);
        }
    }
}
