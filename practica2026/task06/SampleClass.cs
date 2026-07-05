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

    [AttributeUsage(AttributeTargets.Class)]
    public class PluginLoadAttribute : Attribute
    {
        public PluginLoadAttribute()
        {   }
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
}
