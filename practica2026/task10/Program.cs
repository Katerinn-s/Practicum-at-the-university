using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using task06;

namespace task10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Error. Should 1 parametr");
                return;
            }
            string sourceDirectory = args[0];
            var dllFiles = Directory.EnumerateFiles(sourceDirectory, "*.dll");

            foreach (string currentFile in dllFiles)
            {
                string fileName = currentFile.ToString();
                Assembly SampleAssembly;
                SampleAssembly = Assembly.LoadFrom(fileName);
                Type[] types = SampleAssembly.GetTypes();
                foreach (Type t in types)
                {
                    PluginLoadAttribute attribute = (PluginLoadAttribute)Attribute.GetCustomAttribute(t, typeof(PluginLoadAttribute));
                    if (attribute != null)
                    {
                        string fullNameType = t.Namespace+"."+t.Name;
                       
                        Object o = SampleAssembly.CreateInstance(fullNameType);
                        
                        MethodInfo m = SampleAssembly.GetType(fullNameType).GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance);
                        Object ret = m.Invoke(o, new Object[] {  });                 

                    }
                }
            }
          
        }
    }
}
