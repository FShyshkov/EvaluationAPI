using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.Exceptions
{
    public class InvalidResultException : EvaluationException
    {
        public InvalidResultException()
            : base()
        {
        }

        public InvalidResultException(string message)
            : base(message)
        {
        }
    }
}
