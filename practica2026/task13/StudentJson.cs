using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace task13
{
    public static  class StudentJson
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new DateTimeConverter() },
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        public static string Serialize(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            return JsonSerializer.Serialize(student, _options);
        }
        public static Student Deserialize(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) throw new ArgumentException("JSON не может быть пустым", nameof(json));
            return JsonSerializer.Deserialize<Student>(json, _options)
                   ?? throw new InvalidOperationException("Десериализация вернула null");
        }
        public static void SaveToFile(Student student, string filePath)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            var json = Serialize(student);
            File.WriteAllText(filePath, json);
        }
        public static Student LoadFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException($"Файл не найден: {filePath}");

            var json = File.ReadAllText(filePath);
            return Deserialize(json);
        }

    }
}
