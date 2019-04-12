using System;

namespace EvaluationAPI.BLL.Exceptions
{
    public class EvaluationException : Exception
    {
        public EvaluationException()
            : base()
        {
        }

        public EvaluationException(string message)
            : base(message)
        {
        }
    }
}
