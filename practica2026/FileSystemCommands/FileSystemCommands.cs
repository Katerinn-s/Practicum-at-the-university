using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLib;
using System.IO;

namespace FileSystemCommands
{
    public class DirectorySizeCommand: ICommand
    {
        private readonly string _path;
        private long Size { get; set; }
        public DirectorySizeCommand(string path)
        {
            _path = path;
        }
        public void Execute()
        {
            if (!Directory.Exists(_path))
            {
                Console.WriteLine($"Нет папки {_path}");
                Size = 0;
                return;
            }
            long size = GetSize(_path);
            Size = size;
            Console.WriteLine($"Размер папки {_path}: {size}"); return;
        }
        private long GetSize(string path)
        {
            long size = 0;
            foreach (var file in Directory.GetFiles(path)) { 
                FileInfo fi = new FileInfo(file);
                size += fi.Length;
            }
            foreach (var direct in Directory.GetDirectories(path))
                size += GetSize(direct);
            return size;
        }
    }
    public class FindFilesCommand : ICommand
    {
        private readonly string _path;
        private readonly string _mask;
        private List<string> SearchFiles { get; set; }
        public FindFilesCommand(string path, string mask)
        {
            _path = path;
            _mask = mask;
        }
        public void Execute()
        {
            SearchFiles = new List<string>();
            if (!Directory.Exists(_path))
            {
                Console.WriteLine($"Нет папки {_path}");
                SearchFiles = new List<string>();
                return;
            }
            var files = Directory.GetFiles(_path,_mask, SearchOption.AllDirectories);
            SearchFiles = files.ToList();
            if (files.Length==0)
            {
                Console.WriteLine($"Нет файдов по маске{_mask}");
                return; 
            }
            Console.WriteLine($"количество найденных файлов:{files.Length}, маска: {_mask}");
            foreach(var file in files)
            {    
                Console.WriteLine($"{Path.GetFileName(file)}");
            }
        }
    }
}
