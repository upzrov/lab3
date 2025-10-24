using System;

namespace lab_3.BLL
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
