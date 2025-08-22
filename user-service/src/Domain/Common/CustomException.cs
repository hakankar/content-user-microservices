using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class CustomException : Exception
    {
        public ExceptionType Type { get; }

        public CustomException(string message, ExceptionType type) : base(message)
        {
            Type = type;
        }
    }
}
