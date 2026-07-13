using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace task13
{
    public class Subject
    {
        public string Name { get; set; }
        public int Grade { get; set; }
        public override string ToString() => $"{Name}: {Grade}";
    }
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] 
        public DateTime BirthDate { get; set; }

        public List<Subject> Grades { get; set; } = new List<Subject>();

        public override string ToString() => $"{FirstName} {LastName}, {BirthDate:yyyy-MM-dd}";
    }
    public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateTime Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), Format, null);
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, DateTime value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
