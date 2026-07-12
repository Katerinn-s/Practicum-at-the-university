using System.Runtime.Serialization;
using task05;
using Xunit;
namespace task05tests
{
    public class TestClass
    {
        public int PublicField;
        private string _privateField = string.Empty;
        public int Property { get; set; }

        public void Method() { }

        public void Method2(string param) { }
    }

    [SerializableAttribute]
    public class AttributedClass { }

    public class ClassAnalyzerTests
    {
        [Fact]
        public void GetPublicMethods_ReturnsCorrectMethods()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var methods = analyzer.GetPublicMethods();

            Assert.Contains("Method", methods);
        }

        [Fact]
        public void GetAllFields_IncludesPrivateFields()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var fields = analyzer.GetAllFields();

            Assert.Contains("_privateField", fields);
        }

        [Fact]
        public void GetParams()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var param = analyzer.GetMethodParams("Method2");

            Assert.Contains("param", param);
        }

        [Fact]
        public void GetPublicProperties()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var propers = analyzer.GetProperties();

            Assert.Contains("Property", propers);
        }

        [Fact]
        public void HasAtributesTests()
        {
            var analyzer = new ClassAnalyzer(typeof(AttributedClass));
            var attr = analyzer.HasAttribute<SerializableAttribute>();

            Assert.True( attr);
        }
    }
}
