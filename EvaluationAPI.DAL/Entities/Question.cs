using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.DAL.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Name { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
