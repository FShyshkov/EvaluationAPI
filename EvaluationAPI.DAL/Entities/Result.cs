using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.DAL.Entities
{
    public class Result
    {
        public int ResultId { get; set; }
        public string UserName { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int TestResult { get; set; }
    }
}
