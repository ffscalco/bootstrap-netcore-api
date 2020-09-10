using System;
using System.Globalization;

namespace Api.Helpers
{
    public class ValidationException : Exception
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
