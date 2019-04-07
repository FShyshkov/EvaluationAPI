using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.DAL.Entities
{
    public class Test
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Result> Results { get; set; }

        public Test()
        {
            Questions = new List<Question>();
            Results = new List<Result>();
        }
    }
}
