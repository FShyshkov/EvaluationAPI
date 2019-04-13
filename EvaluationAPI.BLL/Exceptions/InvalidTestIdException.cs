using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.Exceptions
{
    public class InvalidTestIdException:EvaluationException
    {
        public InvalidTestIdException()
            : base()
        {
        }

        public InvalidTestIdException(string message)
            : base(message)
        {
        }
    }
}
