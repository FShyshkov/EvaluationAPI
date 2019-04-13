using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.Exceptions
{
    public class UserNameException:EvaluationException
    {
        public UserNameException()
           : base()
        {
        }

        public UserNameException(string message)
            : base(message)
        {
        }
    }
}
