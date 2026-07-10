using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
namespace task06
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Class)]
        public class DisplayNameAttribute : Attribute
        {
            public string DisplayName { get; }

            public DisplayNameAttribute(string displayName)
            {
                DisplayName = displayName;
            }   
        }
    
    [AttributeUsage(AttributeTargets.Class)]
        public class VersionAttribute : Attribute
        {
            public int Minor { get; }
            public int Major { get; }

            public VersionAttribute(int major, int minor)
            {
                Minor = minor;
                Major = major;
            }
        }

    [DisplayName("Пример класса")]
    [Version(1,0)]
    public class SampleClass
    {
        [DisplayName("Тестовый метод")]
        public void TestMethod() { }

        [DisplayName("Числовое свойство")]
        public int Number {  get; }

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PluginLoadAttribute : Attribute
    {
        public PluginLoadAttribute()
        {
        }
    }

    public static class ReflectionHelper {
        public static void PrintTypeInfo(Type t) {
            var attribute = t.GetCustomAttribute<task06.DisplayNameAttribute>();
            if(attribute!=null)
            {
                Console.WriteLine($"Name class: {t.Name}");
            }

            var attribute2 = t.GetCustomAttribute<VersionAttribute>();
            if(attribute!=null)
            {
                Console.WriteLine($"Version class: {attribute2.Major}.{attribute2.Minor}");
            }
            var methods=t.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach (var method in methods)
            {
                Console.WriteLine($"Method name:{method.Name}");
                var attr = method.GetCustomAttribute<DisplayNameAttribute>();
                if (attr!= null)
                {
                    Console.WriteLine($"Display name: {attr.DisplayName}");
                }    
            }
            var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in props)
            {
                Console.WriteLine($"Propertie:{prop.Name}");
                var attr = prop.GetCustomAttribute<DisplayNameAttribute>();
                if (attr != null)
                {
                    Console.WriteLine($"Display name: {attr.DisplayName}");
                }
            }
        }        
    }
}
