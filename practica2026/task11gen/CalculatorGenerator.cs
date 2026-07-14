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

namespace task11gen
{
    public class CalculatorGenerator
    {
        public ICalculator CreateCalculatorInstance(string code)
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
            Assembly ass = Assembly.Load(ms.ToArray());
            Type[] ts = ass.GetTypes();
            Type t = ts.FirstOrDefault(tt => tt.Name == "Calculator");

            string fullNameType = t.Namespace + "." + t.Name;
            Object o = ass.CreateInstance(fullNameType, false,
               BindingFlags.Public | BindingFlags.Instance,
               null,
               null,
               null,
               null);
            return (ICalculator)o;
        }
    }
}
