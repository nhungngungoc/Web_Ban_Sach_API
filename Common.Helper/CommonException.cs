using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class CommonException : Exception
    {
        public int StatusCode { get; }

        public CommonException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
        public CommonException(string message, int statusCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
