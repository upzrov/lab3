using MessagePack;
using System;

namespace lab_3.DAL
{
    [MessagePackObject]
    public class Painter : IPerson
    {
        [Key(0)]
        public required string FirstName { get; set; }

        [Key(1)]
        public required string LastName { get; set; }

        [Key(2)]
        public string? Specialty { get; set; }
    }
}
