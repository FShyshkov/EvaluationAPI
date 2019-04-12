using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationAPI.Models.Requests
{
    public class ResultRequest
    {
        public string UserName { get; set; }
        public int UserResult { get; set; }
    }
}
