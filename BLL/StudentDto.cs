using System;

namespace lab_3.BLL
{
    // Data Transfer Object
    public class StudentDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public int Course { get; set; }
        public required string StudentID { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
