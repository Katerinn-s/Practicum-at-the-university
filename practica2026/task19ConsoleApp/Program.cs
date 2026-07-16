using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using task17;
namespace task19ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
                var server = new ServerThread();

                for (int i = 1; i <= 5; i++)
                {
                    server.AddCommand(new TestCom(i));
                }
                Thread.Sleep(4000);
                var hardStop = new ServerThread.HardStopCommand(server);
                server.AddCommand(hardStop);
                server.Stop();
                Console.WriteLine("Поток остановлен.");
        }
    }
}
