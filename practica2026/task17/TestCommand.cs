using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task17
{
    public class TestCom : ICommand
    {
        private readonly int _id;
        private int _counter = 0;

        public TestCom(int id)
        {
            _id = id;
        }

        public void Execute()
        {
            _counter++;
            Console.WriteLine($"Поток {_id} вызов {_counter}");
            Thread.Sleep(1);
        }
    }
}
