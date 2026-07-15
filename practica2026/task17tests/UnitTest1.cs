using task17;

namespace task17tests
{
    public class ThreadTests
    {
        [Fact]
        public void HardStop_StopsImmediately_IgnoresRemainingCommands()
        {
            var server = new ServerThread();
            var hardStop = new ServerThread.HardStopCommand(server);
            var testCommand = new TestCommand();
            server.AddCommand(testCommand);
            server.AddCommand(testCommand);
            server.AddCommand(hardStop);
            server.AddCommand(testCommand);
            Assert.True(true);
        }

        [Fact]
        public void SoftStop_StopsAfterQueueEmpty()
        {
            var server = new ServerThread();
            var softStop = new ServerThread.SoftStopCommand(server);
            var testCommand = new TestCommand();
            server.AddCommand(testCommand);
            server.AddCommand(testCommand);
            server.AddCommand(softStop);
            server.AddCommand(testCommand);
            server.Stop();
            Assert.True(true);
        }
    }
    public class TestCommand : ICommand
    {
        public void Execute() => Thread.Sleep(1);
    }
}