using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.Exceptions
{
    class InvalidQuestionIDException:EvaluationException
    {
        public InvalidQuestionIDException()
            : base()
        {
        }

        public InvalidQuestionIDException(string message)
            : base(message)
        {
        }
    }
}
