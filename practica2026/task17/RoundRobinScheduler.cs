using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task17
{
    public class RoundRobinScheduler : IScheduler
    {
        private readonly List<ICommand> _commands = new List<ICommand>();
        private int _currentIndex = 0;

        public bool HasCommand() => _commands.Count > 0;

        public ICommand Select()
        {
            if (!HasCommand()) return null;
            var cmd = _commands[_currentIndex];
            _currentIndex = (_currentIndex + 1) % _commands.Count;
            return cmd;
        }

        public void Add(ICommand cmd)
        {
            if (cmd != null && !_commands.Contains(cmd))
                _commands.Add(cmd);
        }

        public void Remove(ICommand cmd)
        {
            if (_commands.Contains(cmd))
                _commands.Remove(cmd);
        }
    }   
}
