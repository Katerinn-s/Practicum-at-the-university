using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace task05
{
    public class ClassAnalyzer
    {
        private Type _type;

        public ClassAnalyzer(Type type)
        {
            _type = type;
        }
        public IEnumerable<string> GetPublicMethods()
        {
            return _type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Select(m => m.Name);
        }

        public IEnumerable<string> GetMethodParams(string methodname)
        {
            var m= _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var x = m.Where(s => s.Name == methodname).First();
            return x.GetParameters().Select(s=>s.Name);
        }

        public IEnumerable<string> GetAllFields()
        {
            return _type.GetFields(BindingFlags.NonPublic| BindingFlags.Public | BindingFlags.Instance).Select(m => m.Name);
        }

        public IEnumerable<string> GetProperties()
        {
            return _type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Select(m => m.Name);
        }

        public bool HasAttribute<T>() where T : Attribute
        {
            var a = _type.GetCustomAttributes(typeof(T), true);
            return a.Length > 0;
        }
    }
}
