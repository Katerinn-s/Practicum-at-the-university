using task13;
using Xunit;
namespace task13tests
{
    public class StudentSerializationTests : IDisposable
    {
        private readonly string _testFilePath = Path.Combine(Path.GetTempPath(), "my_photo.json");
        private readonly Student _testStudent;

        public StudentSerializationTests()
        {
            _testStudent = new Student
            {
                FirstName = "Екатерина",
                LastName = "Яцковская",
                BirthDate = new DateTime(2008, 1, 1),
                Grades = new List<Subject>
                {
                    new Subject { Name = "Математика", Grade = 5 },
                    new Subject { Name = "Физика", Grade = 4 }
                }
            };
        }

        [Fact]
        public void Serialize_Student_ReturnJsonString()
        {
            string json = StudentJson.Serialize(_testStudent);
            Assert.Contains("\"FirstName\": \"Екатерина\"", json);
            Assert.Contains("\"LastName\": \"Яцковская\"", json);
            Assert.Contains("\"BirthDate\": \"2008-01-01\"", json);
            Assert.Contains("\"Name\": \"Математика\"", json);
            Assert.Contains("\"Grade\": 5", json);
        }

        [Fact]
        public void Serialize_NullStudent_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => StudentJson.Serialize(null));
        }

        [Fact]
        public void Deserialize_ValidJson_ReturnsStudentObject()
        {
            string json = StudentJson.Serialize(_testStudent);

            Student deserialized = StudentJson.Deserialize(json);

            Assert.Equal(_testStudent.FirstName, deserialized.FirstName);
            Assert.Equal(_testStudent.LastName, deserialized.LastName);
            Assert.Equal(_testStudent.BirthDate, deserialized.BirthDate);
            Assert.Equal(_testStudent.Grades.Count, deserialized.Grades.Count);
            Assert.Equal(_testStudent.Grades[0].Name, deserialized.Grades[0].Name);
        }

        [Fact]
        public void Deserialize_EmptyJson_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => StudentJson.Deserialize(""));
        }

        [Fact]
        public void SaveToFile_ValidStudent_CreatesFile()
        {
            StudentJson.SaveToFile(_testStudent, _testFilePath);

            Assert.True(File.Exists(_testFilePath));
            string fileContent = File.ReadAllText(_testFilePath);
            Assert.Contains("Екатерина", fileContent);
        }

        [Fact]
        public void LoadExistingFile_ReturnsStudent()
        {
            StudentJson.SaveToFile(_testStudent, _testFilePath);

            Student load = StudentJson.LoadFromFile(_testFilePath);

            Assert.Equal(_testStudent.FirstName, load.FirstName);
            Assert.Equal(_testStudent.LastName, load.LastName);
            Assert.Equal(_testStudent.BirthDate, load.BirthDate);
        }

        [Fact]
        public void LoadExistingFile_ThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => StudentJson.LoadFromFile("my_photo.json"));
        }

        public void Dispose()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }
    }
}
