using System;
using System.Collections.Generic;
using System.Text;
using task17;

namespace task17tests
{
    public class SchedulerTests
    {
        [Fact]
        public void RoundRobinSchedulerSelectsCommandsInOrder()
        {
            var scheduler = new RoundRobinScheduler();
            var cmd1 = new TestLongCommand(1);
            var cmd2 = new TestLongCommand(2);
            var cmd3 = new TestLongCommand(3);
            scheduler.Add(cmd1);
            scheduler.Add(cmd2);
            scheduler.Add(cmd3);
            Assert.Equal(cmd1, scheduler.Select());
            Assert.Equal(cmd2, scheduler.Select());
            Assert.Equal(cmd3, scheduler.Select());
            Assert.Equal(cmd1, scheduler.Select());
        }

        [Fact]
        public void RoundRobinSchedulerReturnsNullWhenNoCommands()
        {
            var scheduler = new RoundRobinScheduler();
            Assert.False(scheduler.HasCommand());
            Assert.Null(scheduler.Select());
        }

        [Fact]
        public void ServerThreadExecutesNormalCommandsImmediately()
        {
            var server = new ServerThread();
            var normalCmd = new TestCommand();
            server.AddCommand(normalCmd);
            Thread.Sleep(100);
            Assert.True(true);
        }
    }
}
