using lab_3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_3.BLL
{
    public class StudentService
    {
        private readonly List<Student> students = [];

        public void AddStudent(StudentDto studentDto)
        {
            if (string.IsNullOrWhiteSpace(studentDto.FullName))
            {
                throw new ValidationException("Full name cannot be empty.");
            }
            if (studentDto.Course < 1 || studentDto.Course > 6)
            {
                throw new ValidationException("Course must be between 1 and 6.");
            }

            var nameParts = studentDto.FullName.Split(' ');
            var student = new Student
            {
                Id = (students.Count != 0 ? students.Max(s => s.Id) : 0) + 1,
                LastName = nameParts.FirstOrDefault() ?? "",
                FirstName = nameParts.Length > 1 ? nameParts[1] : "",
                Course = studentDto.Course,
                StudentID = studentDto.StudentID,
                DateOfBirth = studentDto.DateOfBirth
            };
            students.Add(student);
        }

        public IEnumerable<StudentDto> GetSecondYearStudentsBornInWinter()
        {
            int[] winterMonths = [12, 1, 2];
            return students
                .Where(s => s.Course == 2 && winterMonths.Contains(s.DateOfBirth.Month))
                .Select(MapToDto);
        }

        public IEnumerable<StudentDto> GetAllStudents() => [.. students.Select(MapToDto)];

        public void SaveData(string filePath, IDataProvider<Student> provider)
        {
            provider.Write(students, filePath);
        }

        public void LoadData(string filePath, IDataProvider<Student> provider)
        {
            students.Clear();
            students.AddRange(provider.Read(filePath));
        }

        private StudentDto MapToDto(Student s) => new()
        {
            Id = s.Id,
            FullName = $"{s.LastName} {s.FirstName}",
            Course = s.Course,
            StudentID = s.StudentID,
            DateOfBirth = s.DateOfBirth
        };
    }
}
