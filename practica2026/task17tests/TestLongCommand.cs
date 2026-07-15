using System;
using System.Collections.Generic;
using System.Text;
using task17;

namespace task17tests
{
    public class TestLongCommand : ICommand
    {
        private int _counter = 0;
        private readonly int _id;

        public TestLongCommand(int id) => _id = id;

        public void Execute()
        {
            _counter++;
            Console.WriteLine($"Команда {_id}, вызов {_counter}");

            if (_counter >= 3)
            {
                Console.WriteLine($"Команда {_id} завершена");
            }
        }

        public bool IsComplete => _counter >= 3;
    }
}
