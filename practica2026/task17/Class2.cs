using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task17
{
    public class ServerThread
    {
        private readonly BlockingCollection<ICommand> _queue = new BlockingCollection<ICommand>();
        private readonly Thread _thread;
        private bool _softStop = false;
        public int ThreadId => _thread.ManagedThreadId;
        public ServerThread()
        {
            _thread = new Thread(ProcessCommands);
            _thread.Start();
        }

        public void AddCommand(ICommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            _queue.Add(command);
        }
        public void Stop() => _thread.Join();
        private void ProcessCommands()
        {
            foreach (var command in _queue.GetConsumingEnumerable())
            {
                if (_softStop)
                    break;
                command.Execute();
            }
        }
        public class HardStopCommand : ICommand
        {
            private readonly ServerThread _target;
            public HardStopCommand(ServerThread target)
            {
                _target = target;
            }

            public void Execute()
            {
                if (Thread.CurrentThread.ManagedThreadId != _target.ThreadId)
                    throw new InvalidOperationException("HardStop может быть выполнен только в целевом потоке");
                _target._queue.CompleteAdding();
            }
        }

        public class SoftStopCommand : ICommand
        {
            private readonly ServerThread _target;
            public SoftStopCommand(ServerThread target)
            {
                _target = target;
            }
            public void Execute()
            {
                if (Thread.CurrentThread.ManagedThreadId != _target.ThreadId)
                    throw new InvalidOperationException("SoftStop может быть выполнен только в целевом потоке");
                _target._softStop = true;
                _target._queue.CompleteAdding();
            }
        }
    }
}