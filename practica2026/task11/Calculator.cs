using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11
{
    public interface ICalculator
    {
        int Add(int a, int b);
        int Minus(int a, int b);
        int Mul(int a, int b);
        int Div(int a, int b);
    }

    public class CalculatorGenerator
    {
        private readonly string _calculatorCode =
        "using System; " +
        "public class Calculator : task11.ICalculator " +
        "{ " +
        "    public int Add(int a, int b) => a + b; " +
        "    public int Minus(int a, int b) => a - b; " +
        "    public int Mul(int a, int b) => a * b; " +
        "    public int Div(int a, int b) => a / b; " +
        "}";

        public object CreateCalculatorInstance()
        {
            var assembly = CompileCode(_calculatorCode);
            return assembly.CreateInstance("Calculator");
        }

        private Assembly CompileCode(string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            var references = new[]
            {
        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location),
        MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
        MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
        MetadataReference.CreateFromFile(Assembly.Load("System.Collections.Immutable").Location),
    };

            var compilation = CSharpCompilation.Create(
                "DynamicCalculator",
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);
                if (!result.Success)
                {
                    var errors = new List<string>();
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        errors.Add(diagnostic.ToString());
                    }
                    throw new Exception($"Ошибка компиляции:\n{string.Join("\n", errors)}");
                }

                ms.Seek(0, SeekOrigin.Begin);
                return Assembly.Load(ms.ToArray());
            }
        }
    }

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