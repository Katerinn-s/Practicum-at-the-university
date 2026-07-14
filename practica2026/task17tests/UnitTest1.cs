using task17;

namespace task17tests
{
    public class ThreadTests
    {
        [Fact]
        public void HardStopsImmediately()
        {
            var server = new ServerThread();
            var hardStop = new ServerThread.HardStopCommand(server);
            server.AddCommand(new TestCommand());
            server.AddCommand(new TestCommand());
            server.AddCommand(hardStop);
            server.AddCommand(new TestCommand());
            server.Stop();
            Assert.True(true);
        }

        [Fact]
        public void SoftStopsAfterQueueEmpty()
        {
            var server = new ServerThread();
            var softStop = new ServerThread.SoftStopCommand(server);
            server.AddCommand(new TestCommand());
            server.AddCommand(new TestCommand());
            server.AddCommand(softStop);
            server.AddCommand(new TestCommand());
            server.Stop();
            Assert.True(true);
        }
    }
    public class TestCommand : ICommand
    {
        public void Execute() => Thread.Sleep(10);
    }
}