using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
//using Microsoft.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using task11;
using task11main;

namespace task11appconsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var generator = new CalculatorGenerator();
            string _calculatorCode = "using System; " +
        "public class Calculator : task11.ICalculator " +
        "{ " +
        "    public int Add(int a, int b) => a + b; " +
        "    public int Minus(int a, int b) => a - b; " +
        "    public int Mul(int a, int b) => a * b; " +
        "    public int Div(int a, int b) => a / b; " +
        "}";
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(_calculatorCode);
            string assemblyName = "MyClass";
            MetadataReference[] references = new[] {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location),
                MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "System.Runtime.dll")),
                MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "netstandard.dll"))};

            CSharpCompilation compilation = CSharpCompilation.Create(assemblyName, new[] { syntaxTree }, references);
            var ms = new MemoryStream();
            EmitResult result = compilation.Emit(ms);
            if (!result.Success)
            {
                // Обработка ошибок компиляции
                var errors = new List<string>();
                foreach (var diagnostic in result.Diagnostics)
                {
                    errors.Add(diagnostic.ToString());
                }
                throw new Exception($"Ошибка компиляции:\n{string.Join("\n", errors)}");
            }
            ms.Seek(0, SeekOrigin.Begin);
            //dynamic calculator = generator.GenerateClass(_calculatorCode);

            //int result = calculator.Add(5, 3);
            //Console.WriteLine(result);
        }
    }
}
