using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.DTO
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string[] PossibleAnswers { get; set; }
        public string Answer { get; set; }
        public int TestId { get; set; }
    }
}
