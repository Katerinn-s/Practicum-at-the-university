using FileSystemCommands;
using Xunit;
namespace task08tests
{
    public class FileSystemCommandsTests
    {
        [Fact]
        public void DirectorySizeCommand_ShouldCalculateSize()
        {
            var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
            Directory.CreateDirectory(testDir);
            File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
            File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");
            
            var command = new DirectorySizeCommand(testDir);

            var output = new StringWriter();
            Console.SetOut(output);
            
            command.Execute();

            string outputString = output.ToString();

            Assert.Contains("10", outputString);
            Directory.Delete(testDir, true);
        }

        [Fact]
        public void FindFilesCommand_ShouldFindMatchingFiles()
        {
            var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
            Directory.CreateDirectory(testDir);
            File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
            File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

            var command = new FindFilesCommand(testDir, "*.txt");

            var output = new StringWriter();
            Console.SetOut(output);

            command.Execute();

            string outputString = output.ToString();
            Assert.Contains("1",outputString);
            Directory.Delete(testDir, true);
        }
    }
}
