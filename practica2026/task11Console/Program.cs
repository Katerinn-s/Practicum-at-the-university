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
            var syntaxTree = CSharpSyntaxTree.ParseText(_calculatorCode);
            var references = new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Collections.Immutable.ImmutableArray).Assembly.Location),
            };
            var compilation = CSharpCompilation.Create(
                "DynamicCalculator",
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            MemoryStream ms = new MemoryStream();
            EmitResult result = compilation.Emit(ms);
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
            Assembly ass= Assembly.Load(ms.ToArray());
            Type t = ass.GetType("Calculator");

            Console.WriteLine("test");
        }
    }
}
