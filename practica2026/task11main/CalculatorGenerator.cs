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

namespace task11main
{
    public class CalculatorGenerator
    {
        public ICalculator GenerateClass(string code) {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
            string assemblyName = "MyClass";
            MetadataReference[] references =new[] { 
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
            return (ICalculator)Assembly.Load(ms.ToArray());
        }
    }
}



/*

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
                MetadataReference.CreateFromFile(typeof(System.Collections.Immutable.ImmutableArray).Assembly.Location),
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
        }*/