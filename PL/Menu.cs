using lab_3.BLL;
using lab_3.DAL;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace lab_3.Presentation
{
    public class Menu(StudentService studentService)
    {
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Show All Students");
                Console.WriteLine("3. Show 2nd year students born in winter (Task 9)");
                Console.WriteLine("4. Save Data to File");
                Console.WriteLine("5. Load Data from File");
                Console.WriteLine("0. Exit");
                Console.Write(">> ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": AddStudent(); break;
                        case "2": ShowAllStudents(); break;
                        case "3": ShowFilteredStudents(); break;
                        case "4": SaveData(); break;
                        case "5": LoadData(); break;
                        case "0": return;
                        default: Console.WriteLine("Invalid option."); break;
                    }
                }
                catch (ValidationException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Warning: {ex.Message}");
                    Console.ResetColor();
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Warning: Invalid date or number format.");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        private void AddStudent()
        {
            Console.WriteLine("\nAdd New Student:");
            Console.Write("Full Name (e.g., John Doe): ");
            string fullName = Console.ReadLine() ?? "";

            Console.Write("Course (1-6): ");
            int course = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Student ID (e.g., KB12345): ");
            string studentId = Console.ReadLine() ?? "";

            Console.Write("Date of Birth (YYYY-MM-DD): ");
            DateTime dob = DateTime.ParseExact(Console.ReadLine() ?? "", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var dto = new StudentDto
            {
                FullName = fullName,
                Course = course,
                StudentID = studentId,
                DateOfBirth = dob
            };

            studentService.AddStudent(dto);
            Console.WriteLine("Student added successfully.");
        }

        private void ShowAllStudents()
        {
            var students = studentService.GetAllStudents().ToList();
            Console.WriteLine("\nAll Students ---");
            if (!students.Any())
            {
                Console.WriteLine("No students in memory.");
                return;
            }
            foreach (var s in students)
            {
                Console.WriteLine($"ID: {s.Id}, Name: {s.FullName}, Course: {s.Course}, ID: {s.StudentID}, DoB: {s.DateOfBirth:yyyy-MM-dd}");
            }
        }

        private void ShowFilteredStudents()
        {
            var filtered = studentService.GetSecondYearStudentsBornInWinter().ToList();
            Console.WriteLine("\n2nd Year Students Born in Winter ---");
            if (!filtered.Any())
            {
                Console.WriteLine("No such students found.");
                return;
            }
            foreach (var s in filtered)
            {
                Console.WriteLine($"ID: {s.Id}, Name: {s.FullName}, DoB: {s.DateOfBirth:yyyy-MM-dd}");
            }
            Console.WriteLine($"Total: {filtered.Count}");
        }

        private void SaveData()
        {
            var provider = GetDataProvider();
            Console.Write("Enter filename (e.g., students.json): ");
            string filename = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(filename))
            {
                Console.WriteLine("Save cancelled.");
                return;
            }

            studentService.SaveData(filename, provider);
            Console.WriteLine($"Data saved to {filename}.");
        }

        private void LoadData()
        {
            var provider = GetDataProvider();
            Console.Write("Enter filename to load: ");
            string filename = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
            {
                Console.WriteLine($"File not found: {filename}. Load cancelled.");
                return;
            }

            studentService.LoadData(filename, provider);
            Console.WriteLine($"Data loaded from {filename}.");
        }

        private IDataProvider<Student> GetDataProvider()
        {
            Console.WriteLine("Choose serialization type:");
            Console.WriteLine("1. JSON (Default)");
            Console.WriteLine("2. XML");
            Console.WriteLine("3. MessagePack");
            Console.Write(">> ");
            string type = Console.ReadLine() ?? "";

            switch (type)
            {
                case "2":
                    Console.WriteLine("Using XML provider.");
                    return new XmlDataProvider<Student>();
                case "3":
                    Console.WriteLine("Using MessagePack provider.");
                    return new MessagePackDataProvider<Student>();
                default:
                    Console.WriteLine("Using JSON provider.");
                    return new JsonDataProvider<Student>();
            }
        }
    }
}
