using System;
using System.Collections.Generic;
using System.Text;

namespace WhileLearningCzech.Domain.Helpers.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message)
        : base(message)
        {

        }
    }
}
