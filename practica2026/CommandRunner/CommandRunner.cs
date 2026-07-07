using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using CommandLib;

namespace CommandRunner
{
    internal class CommandRunner
    {
        static void Main(string[] args)
        {
            Assembly SampleAssembly;
            SampleAssembly = Assembly.LoadFrom("FileSystemCommands.dll");
            Type[] types = SampleAssembly.GetTypes();
            Type t = types.FirstOrDefault(tt=>tt.Name== "DirectorySizeCommand");
            
            string fullNameType = t.Namespace + "." + t.Name;
            Object o = SampleAssembly.CreateInstance(fullNameType, false,                                                                   
               BindingFlags.Public | BindingFlags.Instance,                                                           
               null,
               new object[] { "C:\\Windows\\Boot" },
               null,
               null);

            MethodInfo m = SampleAssembly.GetType(fullNameType).GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance);
            Object ret = m.Invoke(o, new Object[] { });

            t = types.FirstOrDefault(tt => tt.Name == "FindFilesCommand");
            fullNameType = t.Namespace + "." + t.Name;
            o = SampleAssembly.CreateInstance(fullNameType, false,                                                                  
               BindingFlags.Public | BindingFlags.Instance,
               null,
               new object[] { "C:\\Windows\\Boot" , "*.sdi" },
               null,
               null);

            m = SampleAssembly.GetType(fullNameType).GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance);
            ret = m.Invoke(o, new Object[] { });
        }
    }
}
