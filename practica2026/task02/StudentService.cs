using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task02
{
    public class Student
    {
        public string Name { get; set; }
        public string Faculty { get; set; }
        public List<int> Grades { get; set; }
    }
    public class StudentService
    {
        private readonly List<Student> _students;

        public StudentService(List<Student> students) => _students = students;
        
        public IEnumerable<Student> GetStudentsByFaculty(string faculty)
            => _students.Where(f=> f.Faculty== faculty);

        public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minAverageGrade)
            => _students.Where(s=>s.Grades.Average() >= minAverageGrade);

        public IEnumerable<Student> GetStudentsOrderedByName()
            => _students.OrderBy(s=>s.Name);

        public ILookup<string, Student> GroupStudentsByFaculty()
            => _students.ToLookup(s=>s.Faculty);

        public string GetFacultyWithHighestAverageGrade()
        {
            var st = _students.Select(g => new { fcl = g.Faculty, av = g.Grades.Average() });
            var fc = st.GroupBy(s => s.fcl).Select(g => new { Facl = g.Key, Av = g.Average(s => s.av) });
            var mx = fc.Max(s => s.Av);
            var res = fc.Where(f => f.Av == mx);
            return res.FirstOrDefault().Facl;
        }
    }
}
