using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.Exceptions
{
    public class QuestionMappingException : EvaluationException
    {
        public QuestionMappingException()
           : base()
        {
        }

        public QuestionMappingException(string message)
            : base(message)
        {
        }
    }
}
