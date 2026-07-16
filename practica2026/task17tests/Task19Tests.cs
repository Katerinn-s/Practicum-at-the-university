using System;
using System.Collections.Generic;
using System.Text;
using task17;

namespace task17tests
{
    public class Task19Tests
    {
        [Fact]
        public void TestCom()
        {
            var server = new ServerThread();
            var output = new StringWriter();
            Console.SetOut(output);

            for (int i = 1; i <= 5; i++)
            {
                server.AddCommand(new TestCom(i));
            }
            Thread.Sleep(5000);
            var hardStop = new ServerThread.HardStopCommand(server);
            server.AddCommand(hardStop);
            server.Stop();
            var result = output.ToString();

            for (int i = 1; i <= 5; i++)
            {
                Assert.Contains($"Поток {i} вызов 3", result);
            }
        }
    }
}
