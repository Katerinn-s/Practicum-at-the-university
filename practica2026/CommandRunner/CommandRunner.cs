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
            ICommand dsc = new FileSystemCommands.DirectorySizeCommand("C:\\Windows\\Boot");
            dsc.Execute();

            dsc = new FileSystemCommands.FindFilesCommand("C:\\Windows\\Boot", "*.sdi");
            dsc.Execute();
        }
    }
}
