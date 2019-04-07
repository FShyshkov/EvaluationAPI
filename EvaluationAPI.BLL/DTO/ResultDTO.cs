using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.DTO
{
    public class ResultDTO
    {
        public int ResultId { get; set; }
        public string UserName { get; set; }
        public int TestId { get; set; }
        public int TestResult { get; set; }
    }
}
