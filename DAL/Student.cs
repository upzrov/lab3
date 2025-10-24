using System;
using MessagePack;

namespace lab_3.DAL
{
    [MessagePackObject]
    public class Student : IPerson
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public required string LastName { get; set; }

        [Key(2)]
        public required string FirstName { get; set; }

        [Key(3)]
        public int Course { get; set; }

        [Key(4)]
        public required string StudentID { get; set; }

        [Key(5)]
        public DateTime DateOfBirth { get; set; }
    }
}

