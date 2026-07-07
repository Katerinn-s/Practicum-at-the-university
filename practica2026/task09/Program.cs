using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using task06;

namespace task09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Error. Should 1 parametr");
                return;
            }
            string l = args[0];
            Assembly SampleAssembly;
            SampleAssembly = Assembly.LoadFrom(l);
            //var n = SampleAssembly.GetReferencedAssemblies();
            //foreach (var a in n) { Console.Write(n.ToString()); }
            Type[] types = SampleAssembly.GetTypes();
            foreach(Type t in types)
            {
                Console.WriteLine(t.Name);
                
                ConstructorInfo[] constructors= t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                Console.WriteLine("  constructors:");
                foreach(ConstructorInfo constructor in constructors)
                {
                    Console.Write("\t");
                    Console.WriteLine(constructor.Name);
                    ParameterInfo[] Params = constructor.GetParameters();
                    Console.WriteLine("\t  Parameters:");
                    foreach (ParameterInfo p in Params)
                    {
                        Console.WriteLine("\t\tParam=" + p.Name.ToString());
                        Console.WriteLine("\t\tType=" + p.ParameterType.ToString());
                    }
                }

                DisplayNameAttribute attribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(t, typeof(DisplayNameAttribute));
                if (attribute == null)
                {
                    Console.WriteLine("\t\t\tThe attribute was not found.");
                }
                else
                {
                    Console.WriteLine("\t  Attributes:");
                    Console.WriteLine($"\t\t\tDisplayName Attribute is: {attribute.DisplayName}.");
                }

                VersionAttribute attribute2 = (VersionAttribute)Attribute.GetCustomAttribute(t, typeof(VersionAttribute));
                if (attribute2 == null)
                {
                    Console.WriteLine("The attribute was not found.");
                }
                else
                {
                    Console.WriteLine($"\t\t\tMinor Attribute is: {attribute2.Minor }.");
                    Console.WriteLine($"\t\t\tMajor Attribute is: {attribute2.Major}.");
                }

                MethodInfo[] methods = t.GetMethods();
                Console.WriteLine("  methods:");
                foreach (MethodInfo m in methods)
                {
                    Console.Write("\t");
                    Console.WriteLine(m.Name);
                    ParameterInfo[] Params = m.GetParameters();
                    Console.WriteLine("\t  Parameters:");
                    foreach (ParameterInfo p in Params)
                    {
                        Console.WriteLine("\t\tParam=" + p.Name.ToString());
                        Console.WriteLine("\t\tType=" + p.ParameterType.ToString());
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
